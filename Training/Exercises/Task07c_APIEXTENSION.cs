using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.APIExtensions;
using commercetools.Sdk.Domain.GraphQL;
using commercetools.Sdk.HttpApi.CommandBuilders;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// Create API Extension on Cart Update
    /// </summary>
    public class Task07C : IExercise
    {
        private readonly IClient _client;
        
        public Task07C(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //create a trigger on cart update
           

            //create AwsLambdaDestination
            
            //create extensionDraft
            
            //create the extension
           
            throw new NotImplementedException();
        }
    }
}