using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Products;
using commercetools.Sdk.Api.Models.ProductSelections;
using commercetools.Sdk.Api.Models.Stores;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using System.Collections.Generic;
using System;

namespace Training.Services
{
    public class ProductSelectionService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public ProductSelectionService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// Gets product selection by key
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <returns></returns>
        public async Task<IProductSelection> GetProductSelectionByKey(string productSelectionKey){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(productSelectionKey)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// Creates product selection
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IProductSelection> CreateProductSelection(string productSelectionKey, LocalizedString name){
            throw new NotImplementedException();
        }   

        /// <summary>
        /// Adds product to product selection
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <param name="productKey"></param>
        /// <returns></returns>
        public async Task<IProductSelection> AddProductToProductSelection(string productSelectionKey, string productKey){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets products in a product selection
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <returns></returns>
        public async Task<IProductSelectionProductPagedQueryResponse> GetProductSelectionProductByKey(string productSelectionKey){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(productSelectionKey)
                .Products()
                .Get()
                .WithExpand("product")
                .ExecuteAsync();
        }

    }
}