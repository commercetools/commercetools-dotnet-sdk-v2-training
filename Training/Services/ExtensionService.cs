using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Extensions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class ExtensionService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public ExtensionService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// returns GraphQL response
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        public async Task<IExtension> createExtension(string key,IExtensionDestination destination, 
            List<IExtensionTrigger> triggers)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Extensions()
                .Post(
                    new ExtensionDraft
                    {
                        Key = key,
                        Destination = destination,
                        Triggers = triggers
                    }
                )
                .ExecuteAsync();
        }
    }
}