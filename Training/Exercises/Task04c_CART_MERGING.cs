using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Customers;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Anonymous Cart Merging
    /// </summary>
    public class Task04C : IExercise
    {
        private readonly IClient _client;
        
        public Task04C(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
           // Create a customer
           var customer = await CreateACustomer();
           Console.WriteLine($"customer created with Id {customer.Id}");
           
           //Create Cart for this customer

           var cart = await CreateACart(null, customer.Id);
           Console.WriteLine($"cart for customer created with Id {cart.Id}");

           // Create Anonymous cart
           var anonymousCart = await CreateACart("123456789", null);
           Console.WriteLine($"anonymous cart created with Id {anonymousCart.Id}");
           
           anonymousCart = await AddProductToACartBySku(anonymousCart, "9812", 4);
           
           var result = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
               .Login()
               .Post(new CustomerSignin
               {
                   AnonymousCartId = anonymousCart.Id,
                   AnonymousCartSignInMode = IAnonymousCartSignInMode.MergeWithExistingCustomerCart
               }).ExecuteAsync();
           
           //LineItems of the anonymous cart will be copied to the customer’s active cart that has been modified most recently.
           var currentCustomerCart = result?.Cart as Cart;
           var lineItem = currentCustomerCart?.LineItems[0];
           Console.WriteLine($"SKU: {lineItem.Variant.Sku}, Quantity: {lineItem.Quantity}");
        }

        private async Task<ICustomer> CreateACustomer()
        {
            var result = await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .Post(new CustomerDraft
                {
                    Email = "me@me4",
                    Password = "password"
                }).ExecuteAsync() as CustomerSignInResult;
            return result?.Customer;
        }
        
        /// <summary>
        /// Create A Cart, if customerId is null, then it's anonymous cart, else it's a cart for customer
        /// </summary>
        /// <param name="anonymousId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<Cart> CreateACart(string anonymousId = null, string customerId = null)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts().Post(
                new CartDraft
                {
                    CustomerId = customerId,
                    AnonymousId = anonymousId,
                    Currency = "EUR",
                    Country = "DE",
                    DeleteDaysAfterLastModification = 90
                }).ExecuteAsync();
        }
        
        private async Task<Cart> AddProductToACartBySku(Cart cart, string sku, long quantity)
        {
            var cartUpdate = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartAddLineItemAction
                    {
                        Sku = sku,
                        Quantity = quantity
                    }
                }
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .WithId(cart.Id)
                .Post(cartUpdate)
                .ExecuteAsync();
        }
    }
}