using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.GraphQl;
using commercetools.Api.Models.Products;
using commercetools.Api.Models.Types;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class TypeService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public TypeService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// Creates a new custom type
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="resourceTypeIds"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<IType> CreateCustomType(string key,LocalizedString name, List<IResourceTypeId> resourceTypeIds, List<IFieldDefinition> fields)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Types()
                .Post( 
                    new TypeDraft
                    {
                        Key = key,
                        Name = name,
                        ResourceTypeIds = resourceTypeIds,
                        FieldDefinitions = fields
                    }
                )
                .ExecuteAsync();
        }
    }
}