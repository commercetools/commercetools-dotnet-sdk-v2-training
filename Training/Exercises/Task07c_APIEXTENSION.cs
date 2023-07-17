using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Extensions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// Host your extension code in GCP/AWS
    /// Create API Extension on Order Create
    /// </summary>
    public class Task07C : IExercise
    {
        private readonly IClient _client;

        public Task07C(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
        }

        public async Task ExecuteAsync()
        {
            // TODO: create an Extension trigger (on Order Create)


            //create destination
            var destination = new GoogleCloudFunctionDestination()
            {
                Type = "GoogleCloudFunction",
                Url = ""
            };
            

            // TODO: CREATE the extension
            
            //Console.WriteLine($"extension created with Id {extension.Id}");
        }




        /// <summary>
        /// creates a new extension
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        private async Task<IExtension> CreateExtension(string key, IExtensionDestination destination,
            List<IExtensionTrigger> triggers)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Extensions()
                .Post(
                    new ExtensionDraft
                    {
                        Destination = destination,
                        Key = key,
                        Triggers = triggers
                    }
                )
                .ExecuteAsync();
        }
    }
}