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
            var trigger = new Trigger
            {
                ResourceTypeId = ExtensionResourceType.Cart,
                Actions = new List<TriggerType>
                {
                    TriggerType.Update
                }
            };

            //create AwsLambdaDestination
            var destination = new AwsLambdaDestination
            {
                AccessKey = "key",
                AccessSecret = "secert",
                Arn = "arn"
            };
            
            //create extensionDraft
            var extensionDraft = new ExtensionDraft
            {
                Key = "AddLeafletsExtension",
                Destination = destination,
                Triggers = new List<Trigger> {trigger}
            };
            
            //create the extension
            var extension = await _client.ExecuteAsync(new CreateCommand<Extension>(extensionDraft));
            
            Console.WriteLine($"extension created with Id {extension.Id}");
        }
    }
}