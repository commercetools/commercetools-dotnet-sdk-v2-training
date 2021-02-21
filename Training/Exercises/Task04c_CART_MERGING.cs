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
        private readonly string _customerPassword = "password";
        
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
           
           //Add Product to cart
           cart = await AddProductToACartBySku(cart, "A0E200000002E49", 1);

           // Create Anonymous cart
           var anonymousCart = await CreateACart("123456789", null);
           Console.WriteLine($"anonymous cart created with Id {anonymousCart.Id}");
           
           //Add Product to the Anonymous cart
           anonymousCart = await AddProductToACartBySku(anonymousCart, "A0E200000001WG3", 4);
           
           //Decide on a merging strategy
           
           
           //LineItems of the anonymous cart will be copied to the customer’s active cart that has been modified most recently.
           
        }

        private async Task<ICustomer> CreateACustomer()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Create A Cart, if customerId is null, then it's anonymous cart, else it's a cart for customer
        /// </summary>
        /// <param name="anonymousId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<Cart> CreateACart(string anonymousId = null, string customerId = null)
        {
            throw new NotImplementedException();
        }
        
        private async Task<Cart> AddProductToACartBySku(Cart cart, string sku, long quantity)
        {
            throw new NotImplementedException();
        }
    }
}