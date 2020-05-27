using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.GraphQL;
using commercetools.Sdk.Domain.Subscriptions;
using commercetools.Sdk.Domain.Types;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// Create a subscription for customer change requests.
    /// </summary>
    public class Task08A : IExercise
    {
        private readonly IClient _client;
        
        public Task08A(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
           //create SqsDestination
           var destination = new SqsDestination(
               "key",
               "secert",
               "url",
               "eu-central-1");
           
           //create a subscription draft
           var subscriptionDraft = new SubscriptionDraft
           {
               Key = "OnCustomerChangedSubscription",
               Destination = destination,
               Changes = new List<ChangeSubscription>
               {
                   new ChangeSubscription
                   {
                       ResourceTypeId = ResourceTypeId.Customer.GetDescription()
                   }
               }
           };
           
           //create a subscription
           var subscription = await _client.ExecuteAsync(new CreateCommand<Subscription>(subscriptionDraft));
           
           Console.WriteLine($"a new subscription created with Id {subscription.Id}");
        }
    }
}