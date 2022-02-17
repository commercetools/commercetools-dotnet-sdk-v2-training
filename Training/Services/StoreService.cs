using System;
using System.Threading.Tasks;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Customers;
using commercetools.Api.Models.ProductSelections;
using commercetools.Api.Models.Stores;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new cart for a customer in a store with default shipping address
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="storeKey"></param>
        /// <returns></returns>
        public async Task<ICart> CreateInStoreCart(ICustomer customer, string storeKey)
        {
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets product selection for a store
        /// </summary>
        /// <param name="productSelectionKey"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public async Task<IStore>  AddProductSelectionToStore(string productSelectionKey, IStore store){
            throw new NotImplementedException();
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