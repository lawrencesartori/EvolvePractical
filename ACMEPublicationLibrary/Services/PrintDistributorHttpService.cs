using ACMEPublicationLibrary.Database;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Services
{
    public class PrintDistributorHttpService
    {
        private readonly IHttpClientFactory _client;
        private readonly PublicationService _publicationService;
        private readonly ILogger<PrintDistributorHttpService> _logger;
        public PrintDistributorHttpService(IHttpClientFactory client, PublicationService publicationService, ILogger<PrintDistributorHttpService> logger)
        {
            _client = client;
            _publicationService = publicationService;
            _logger = logger;
        }

        public async Task PostToDistributor(TblSubscription sub, TblPrintDistributor distributor)
        {
            HttpRequestMessage? message = null;

            switch (distributor.PrintDistributorId)
            {
                /**Example case of distributor 1, They have a json post with a bearer token**/
                case 1:
                    {
                        message = GetRequestMessage(HttpMethod.Post, JsonConvert.SerializeObject(sub), distributor.RequestEndpoint);
                        if (!string.IsNullOrEmpty(distributor.RequestToken)) 
                        {
                            message.Headers.Add("Authorization", $"Bearer:{distributor.RequestToken}");
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if(message == null)
            {
                _logger.LogError($"Failed to create message for distributor {distributor.PrintDistributorId} for subscription {sub.SubscriptionId}");
                return;
            }

            await SendRequest(message);
        }

        private HttpRequestMessage GetRequestMessage(HttpMethod method, string content, string requestUrl)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUrl)
            };

            if (method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }

        private async Task SendRequest(HttpRequestMessage message)
        {
            var client = _client.CreateClient();
            var response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Call to {message.RequestUri} failed - {response.StatusCode} - {response.Content}");
            }
        }
    }
}
