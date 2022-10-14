using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Subscriptions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Models.Types;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// Create a subscription
    /// </summary>
    public class Task08A : IExercise
    {
        private readonly IClient _client;

        public Task08A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
        }

        public async Task ExecuteAsync()
        {
            //create destination
            var destination = new GoogleCloudPubSubDestination()
            {
                Type="GoogleCloudPubSub",
                ProjectId = "ct-support",
                Topic = "topic-example"
            };


            // TODO: CREATE the subscription

            //Console.WriteLine($"a new subscription created with Id {subscription.Id}");
        }

        /// <summary>
        /// creates a subscription
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        private async Task<ISubscription> CreateSubscription(string key,
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