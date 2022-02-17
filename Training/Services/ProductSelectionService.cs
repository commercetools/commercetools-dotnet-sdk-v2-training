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

        public async Task<IProductSelection> GetProductSelectionByKey(string productSelectionKey){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(productSelectionKey)
                .Get()
                .ExecuteAsync();
        }
            
            

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