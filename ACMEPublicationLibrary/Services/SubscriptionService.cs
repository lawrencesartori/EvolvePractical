using ACMEPublicationLibrary.Database;
using ACMEPublicationLibrary.Extensions;
using ACMEPublicationLibrary.Models.Filters;
using ACMEPublicationLibrary.Store;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Services
{
    public class SubscriptionService
    {
        private readonly SubscriptionStore _subscriptionStore;
        private readonly PrintDistributorHttpService _printDistributorHttpService;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(SubscriptionStore subscriptionStore, PrintDistributorHttpService printDistributorHttpService, ILogger<SubscriptionService> logger)
        {
            _subscriptionStore = subscriptionStore;
            _printDistributorHttpService = printDistributorHttpService;
            _logger = logger;
        }

        public List<TblSubscription> GetActiveSubscriptions(DateTime dateStart, DateTime dateEnd)
        {
            var filter = new SubscriptionFilter 
            {
                ActiveOnly = true,
                DateStart = dateStart,
                DateEnd = dateEnd,
                IncludeDeliveryAddressDependencies = true
            };

            return _subscriptionStore.GetSubscriptions(filter);
        }
    }
}
