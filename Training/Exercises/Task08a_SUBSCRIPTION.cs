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
           
           //create a subscription draft
           
           //create a subscription
           
           throw new NotImplementedException();
        }
    }
}