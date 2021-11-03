using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Subscriptions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// Create a subscription
    /// </summary>
    public class Task08A : IExercise
    {
        private readonly IClient _client;
        
        public Task08A(IClient client)
        {
            this._client = client;
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
           
            //create a subscription draft (orderCreated)
            
           
            //create a subscription
            
           
            //Console.WriteLine($"a new subscription created with Id {subscription.Id}");
        }
    }
}