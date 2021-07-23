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
           
            //create a subscription draft
            var subscriptionDraft = new SubscriptionDraft
            {
                Key = "subscriptionSampleForSendingConfirmationEmails",
                Destination = destination,
                Messages = new List<IMessageSubscription>
                {
                    new MessageSubscription
                    {
                        ResourceTypeId = "order",
                        Types = new List<string> { "OrderCreated" }
                    }
                }
            };
           
            //create a subscription
            var subscription = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Subscriptions()
                .Post(subscriptionDraft)
                .ExecuteAsync();
           
            Console.WriteLine($"a new subscription created with Id {subscription.Id}");
        }
    }
}