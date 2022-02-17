using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Extensions;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Create API Extension on Order Create
    /// </summary>
    public class Task07C : IExercise
    {
        private readonly IClient _client;
        private readonly ExtensionService _extensionService;
        
        public Task07C(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _extensionService = new ExtensionService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            // extension trigger
            var trigger = new ExtensionTrigger
            {
                ResourceTypeId = IExtensionResourceTypeId.Order,
                Actions = new List<IExtensionAction> { IExtensionAction.Create }
            };

            // extension destination
            var destination = new ExtensionHttpDestination()
            {
                Type = "HTTP",
                Url = "https://europe-west3-ct-support.cloudfunctions.net/training-extensions-sample"
            };
            
            //create the extension
            var extension = await _extensionService.CreateExtension(
                "order-checker",
                destination,
                new List<IExtensionTrigger> {trigger}
            );
            
            Console.WriteLine($"extension created with Id {extension.Id}");
        }
    }
}