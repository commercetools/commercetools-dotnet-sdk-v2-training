using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models;
using commercetools.Api.Models.Subscriptions;
using commercetools.Api.Models.Types;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Create a subscription for customer change requests.
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
            //create SqsDestination
            var destination = new SqsDestination
            {
                QueueUrl = "",
                Region = "",
                AccessKey = "",
                AccessSecret = ""
            };
           
            //create a subscription draft
            var subscriptionDraft = new SubscriptionDraft
            {
                Key = "OnCustomerChangedSubscription",
                Destination = destination,
                Changes = new List<IChangeSubscription>
                {
                    new ChangeSubscription
                    {
                        ResourceTypeId = IResourceTypeId.Customer.JsonName
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