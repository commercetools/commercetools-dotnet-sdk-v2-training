using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Extensions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

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
            //create a trigger
            var trigger = new ExtensionTrigger
            {
                ResourceTypeId = IExtensionResourceTypeId.Order,
                Actions = new List<IExtensionAction> { IExtensionAction.Create }
            };

            //create destination
            var destination = new ExtensionHttpDestination()
            {
                Type = "HTTP",
                Url = "https://europe-west3-ct-support.cloudfunctions.net/training-extensions-sample"
            };
            
            //create extensionDraft
            var extensionDraft = new ExtensionDraft
            {
                Key = "orderChecker",
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