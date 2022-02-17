using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Products;
using commercetools.Api.Models.ProductSelections;
using commercetools.Api.Models.Stores;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using System.Collections.Generic;

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
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .Post(
                    new ProductSelectionDraft{
                        Key = productSelectionKey,
                        Name = name
                    }
                )
                .ExecuteAsync();
        }            
            
        /// <summary>
        /// Adds product to product selection
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <param name="productKey"></param>
        /// <returns></returns>
        public async Task<IProductSelection> AddProductToProductSelection(string productSelectionKey, string productKey){
            IProductSelection productSelection = await GetProductSelectionByKey(productSelectionKey);
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(productSelectionKey)
                .Post( 
                    new ProductSelectionUpdate{
                        Version = productSelection.Version,
                        Actions = new List<IProductSelectionUpdateAction>{
                            new ProductSelectionAddProductAction{
                                Product = new ProductResourceIdentifier{Key = "tulip-seed-product"}
                            }
                        }
                    }
                )
                .ExecuteAsync();
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