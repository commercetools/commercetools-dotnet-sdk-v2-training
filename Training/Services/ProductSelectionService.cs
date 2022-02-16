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

        public async Task<IProductSelection> createProductSelection(string key, LocalizedString name){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .Post(
                    new ProductSelectionDraft{
                        Key = key,
                        Name = name
                    }
                )
                .ExecuteAsync();
        }

        public async Task<IProductSelection> getProductSelectionByKey(string key){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(key)
                .Get()
                .ExecuteAsync();
        }
            
            

        public async Task<IProductSelection> addProductToProductSelection(string key, string productKey){
            IProductSelection productSelection = await getProductSelectionByKey(key);
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductSelections()
                .WithKey(key)
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

            
        public async Task<IStore>  addProductSelectionToStore(string key,IStore store){
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Stores()
                .WithId(store.Id)
                .Post(
                    new StoreUpdate{
                        Version = store.Version,
                        Actions = new List<IStoreUpdateAction>{
                            new StoreSetProductSelectionsAction{
                                ProductSelections = new List<IProductSelectionSettingDraft> {
                                    new ProductSelectionSettingDraft{
                                        ProductSelection = new ProductSelectionResourceIdentifier{Key = key},
                                        Active = true
                                    }
                                }
                            }
                        }
                    }
                )
                .ExecuteAsync();
        }        
    }
}