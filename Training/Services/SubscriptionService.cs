using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Subscriptions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class SubscriptionService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public SubscriptionService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// returns GraphQL response
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task<ISubscription> CreateSubscription(string key, 
            IDestination destination, 
            List<IMessageSubscription> messages)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Subscriptions()
                .Post(
                    new SubscriptionDraft
                    {
                        Key = key,
                        Destination = destination,
                        Messages = messages
                    }
                )
                .ExecuteAsync();
        }
    }
}