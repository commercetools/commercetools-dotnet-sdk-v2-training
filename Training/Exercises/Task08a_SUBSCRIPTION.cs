using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Subscriptions;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Create a subscription
    /// </summary>
    public class Task08A : IExercise
    {
        private readonly IClient _client;
        SubscriptionService _subscriptionService;

        public Task08A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _subscriptionService = new SubscriptionService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            //create destination
            var destination = new GoogleCloudPubSubDestination()
            {
                Type="GoogleCloudPubSub",
                ProjectId = "ct-support",
                Topic = "training-subscription-sample"
            };
            
           
            // TODO: CREATE the subscription
            
           
            //Console.WriteLine($"a new subscription created with Id {subscription.Id}");
        }
    }
}