using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Extensions;
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
        public async Task<IExtension> CreateExtension(string key,IExtensionDestination destination, 
            List<IExtensionTrigger> triggers)
        {
            throw new NotImplementedException();
        }
    }
}