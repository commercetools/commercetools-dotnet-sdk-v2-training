using System;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.GraphQl;
using commercetools.Sdk.Api.Models.Products;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class GraphQLService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public GraphQLService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// returns GraphQL response
        /// </summary>
        /// <param name="graphRequest"></param>
        /// <returns></returns>
        public async Task<IGraphQLResponse> GetGraphQLQueryResponse(IGraphQLRequest graphRequest)
        {
            throw new NotImplementedException();
        }
    }
}