using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Sdk.Api.Models.ProductSelections;
using commercetools.Sdk.Api.Models.Stores;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Extensions;
using System.Collections.Generic;

namespace Training.Services
{
    public class StoreService
    {
        private readonly IClient _berlinStoreClient;
        private readonly string _projectKey;
        
        public StoreService(IClient client, string projectKey)
        {
            _berlinStoreClient = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// Get customers in a store
        /// </summary>
        /// <param name="storeKey"></param>
        /// <returns></returns>
        public async Task<ICustomerPagedQueryResponse> GetCustomersInStore(string storeKey)
        {
            return await _berlinStoreClient.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .InStoreKeyWithStoreKeyValue(storeKey)
                .Customers()
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// Get customers in a store
        /// </summary>
        /// <param name="storeKey"></param>
        /// <param name="storeKey"></param>
        /// <returns></returns>
        public async Task<ICustomer> GetCustomerInStoreByKey(string customerKey, string storeKey)
        {
            return await _berlinStoreClient.WithApi().WithProjectKey(_projectKey)
                .InStoreKeyWithStoreKeyValue(storeKey)
                .Customers()
                .WithKey(customerKey)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// Create a new cart for a customer in a store with default shipping address
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="storeKey"></param>
        /// <returns></returns>
        public async Task<ICart> CreateInStoreCart(string customerKey, string storeKey)
        {
            var customer = await GetCustomerInStoreByKey(customerKey,storeKey);

            var defaultShippingAddress = customer.GetDefaultShippingAddress();
            
            var cartDraft = new CartDraft
            {
                CustomerId = customer.Id,
                CustomerEmail = customer.Email,
                Currency = "EUR",
                Country = defaultShippingAddress.Country,
                ShippingAddress = defaultShippingAddress,
                DeleteDaysAfterLastModification = 90,
                InventoryMode = IInventoryMode.ReserveOnOrder,
                // Store = new StoreResourceIdentifier { Key = storeKey}
            };
            return await _berlinStoreClient.WithApi().WithProjectKey(Settings.ProjectKey)
                .InStoreKeyWithStoreKeyValue(storeKey)
                .Carts()
                .Post(cartDraft)
                .ExecuteAsync();
        }

        /// <summary>
        /// Sets product selection for a store
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public async Task<IStore>  AddProductSelectionsToStore(string productSelectionKey,IStore store){
            return await _berlinStoreClient.WithApi().WithProjectKey(Settings.ProjectKey)
                .Stores()
                .WithId(store.Id)
                .Post(
                    new StoreUpdate{
                        Version = store.Version,
                        Actions = new List<IStoreUpdateAction>{
                            new StoreSetProductSelectionsAction{
                                ProductSelections = new List<IProductSelectionSettingDraft> {
                                    new ProductSelectionSettingDraft{
                                        ProductSelection = new ProductSelectionResourceIdentifier{Key = productSelectionKey},
                                        Active = true
                                    }
                                }
                            }
                        }
                    }
                )
                .ExecuteAsync();
        }
        
        /// <summary>
        /// Gets product selection assignments for a store
        /// </summary>
        /// <param name="storeKey"></param>
        /// <returns></returns>
        public async Task<IProductsInStorePagedQueryResponse>  GetProductsInStore(string storeKey){
            return await _berlinStoreClient.WithApi().WithProjectKey(Settings.ProjectKey)
                .InStoreKeyWithStoreKeyValue(storeKey)
                .ProductSelectionAssignments()
                .Get()
                .ExecuteAsync();
        }
        
    }
}