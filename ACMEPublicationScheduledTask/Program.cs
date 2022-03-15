using ACMEPublicationLibrary;
using ACMEPublicationLibrary.Database;
using ACMEPublicationLibrary.Extensions;
using ACMEPublicationLibrary.Services;
using ACMEPublicationLibrary.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ACMEPublicationScheduledTask
{
    public class Program
    {
        private static IConfiguration? _configuration { get; set; }

        private static IServiceProvider? _provider;

        private static SubscriptionService? _subscriptionService { get; set; }
        private static PublicationService? _publicationService { get; set; }

        private static PrintDistributorHttpService? _printDistributorHttpService { get; set; }

        public static async Task Main(string[] args)
        {
           InitializeLoggingAndServices();
           if(_subscriptionService == null || _publicationService == null || _printDistributorHttpService == null)
           {
                LogError($"One or more services failed to initialise. Please investigate.");
                return;
            }
           else
           {
              await SendOutMonthlyPublications();
           }
        }

        private static async Task SendOutMonthlyPublications()
        {
            var dateStart = DateTime.Now.GetStartOfMonth();
            var dateEnd = dateStart.GetEndOfMonth();
            LogInfo($"Starting Monthly Publication Send Out Task for {dateStart.ToDateFormatString()} - {dateEnd.ToDateFormatString()}");

            var currentActiveSubscriptions = _subscriptionService.GetActiveSubscriptions(dateStart, dateEnd);
            var printDistributors = _publicationService.GetAllCountryPrintDistributions();
            var postsToMake = new List<Task>();
            foreach (var sub in currentActiveSubscriptions)
            {
                var relevantPublicationId = sub.PublicationId;
                var relevantCountryId = sub.DeliveryAddress.State.CountryId;
                if(relevantPublicationId <= 0)
                {
                    LogError($"Subscription {sub.SubscriptionId} has an invalid publication id {relevantPublicationId}");
                    continue;
                }

                if (relevantCountryId <= 0)
                {
                    LogError($"Subscription {sub.SubscriptionId} has an invalid country id {relevantCountryId}");
                    continue;
                }

                printDistributors.TryGetValue((relevantCountryId, relevantPublicationId), out var relevantDistributor);
                if(relevantDistributor == null)
                {
                    LogError($"Subscription {sub.SubscriptionId} does not have a distributor for country {relevantCountryId} and publication {relevantPublicationId}");
                    continue;
                }


                postsToMake.Add(_printDistributorHttpService.PostToDistributor(sub, relevantDistributor));
            }

            var totalToRun = postsToMake.Count / Constants.MaxTasksToRunAtOnce;
            var oneMore = postsToMake.Count % Constants.MaxTasksToRunAtOnce != 0;
            if (oneMore)
            {
                totalToRun += 1;
            }

            for(var i = 0; i<totalToRun; i++)
            {
                var tasksToRun = postsToMake.Skip(i * Constants.MaxTasksToRunAtOnce).Take(Constants.MaxTasksToRunAtOnce).ToList();
                if (tasksToRun.Any())
                {
                    var result = Task.WhenAll(tasksToRun);
                    try 
                    {
                        await result; 
                    }
                    catch
                    {

                    }

                    if(result.Status == TaskStatus.RanToCompletion)
                    {
                        LogInfo($"All Posts {i * Constants.MaxTasksToRunAtOnce} - {(i * Constants.MaxTasksToRunAtOnce)+ Constants.MaxTasksToRunAtOnce} succeeded");
                    }
                    else
                    {
                        LogError($"Some Posts in batch {i * Constants.MaxTasksToRunAtOnce} - {(i * Constants.MaxTasksToRunAtOnce) + Constants.MaxTasksToRunAtOnce} failed. Please check logs.");
                    }
                }
            }
            
        }

        private static void LogError(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Log.Error(errorMessage);
        }

        private static void LogInfo(string message)
        {
            Console.WriteLine(message);
            Log.Information(message);
        }

        private static void InitializeLoggingAndServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

			var hostBuilder = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
                    services.AddSingleton(_configuration);
                    //services.AddDbContext<PublicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DSN")));
                    services.AddScoped<PublicationDbContext>();
                    services.AddTransient<SubscriptionStore>();
                    services.AddTransient<PublicationStore>();
                    services.AddTransient<SubscriptionService>();
                    services.AddTransient<PublicationService>();
                    services.AddTransient<PrintDistributorHttpService>();
                    services.AddHttpClient();
				}).ConfigureAppConfiguration((hostingContext, cfg) =>
				{
				}).UseSerilog().UseConsoleLifetime();

            _provider = hostBuilder.Build().Services;

            _subscriptionService = _provider.GetService<SubscriptionService>();
            _publicationService = _provider.GetService<PublicationService>();
            _printDistributorHttpService = _provider.GetService<PrintDistributorHttpService>();
        }
    }
}