using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Extensions;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Create API Extension on Order Create
    /// </summary>
    public class Task07C : IExercise
    {
        private readonly IClient _client;
        
        public Task07C(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            //create a trigger on order create
            var trigger = new ExtensionTrigger
            {
                ResourceTypeId = IExtensionResourceTypeId.Order,
                Actions = new List<IExtensionAction> { IExtensionAction.Create }
            };

            //create AwsLambdaDestination
            var destination = new ExtensionAWSLambdaDestination
            {
                AccessKey = "key",
                AccessSecret = "secert",
                Arn = "arn"
            };
            
            //create extensionDraft
            var extensionDraft = new ExtensionDraft
            {
                Key = "mhPlantCheck777",
                Destination = destination,
                Triggers = new List<IExtensionTrigger> {trigger}
            };
            
            //create the extension
            var extension = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Extensions()
                .Post(extensionDraft)
                .ExecuteAsync();
            
            Console.WriteLine($"extension created with Id {extension.Id}");
        }
    }
}