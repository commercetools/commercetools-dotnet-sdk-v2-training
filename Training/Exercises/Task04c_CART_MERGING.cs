using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// Anonymous Cart Merging
    /// </summary>
    public class Task04C : IExercise
    {
        private readonly IClient _client;
        
        public Task04C(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
           // Create a customer
           Customer customer = null;
           Console.WriteLine($"customer created with Id {customer.Id}");
           
           //Create Cart for this customer

           Cart cart = null;
           Console.WriteLine($"cart for customer created with Id {cart.Id}");

           // Create Anonymous cart
           Cart anonymousCart = null;
           Console.WriteLine($"anonymous cart created with Id {anonymousCart.Id}");
           
           //AddProductToACartBySku
           

           //Login Customer with anonymous cartId
           
           
           //LineItems of the anonymous cart will be copied to the customer’s active cart that has been modified most recently.
           
        }

        private async Task<Customer> CreateACustomer()
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