using System;
using System.Threading.Tasks;
using commercetools.Api.Models.Products;
using commercetools.Api.Models.ProductTypes;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class SearchService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public SearchService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }
        /// <summary>
        /// returns product type by key
        /// </summary>
        /// <param name="productTypeKey"></param>
        /// <returns></returns>
        public async Task<IProductType> GetProductTypeByKey(string productTypeKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductTypes()
                .WithKey(productTypeKey)
                .Get()
                .ExecuteAsync();
        }


        /// <summary>
        /// returns paged product projections search results with facets
        /// </summary>
        /// <param name="filterQuery"></param>
        /// <param name="facet"></param>
        /// <returns></returns>
        public async Task<IProductProjectionPagedSearchResponse> GetSearchResults(
            string filterQuery = null, 
            string facet = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns paged product query results
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IProductPagedQueryResponse> GetPagedResults(
            string where,
            int pageSize)
        {
            throw new NotImplementedException();
        }
        
    }
}