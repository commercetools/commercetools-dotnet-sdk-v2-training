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
           var customer = await CreateACustomer();
           Console.WriteLine($"customer created with Id {customer.Id}");
           
           //Create Cart for this customer

           var cart = await CreateACart(null, customer.Id);
           Console.WriteLine($"cart for customer created with Id {cart.Id}");

           // Create Anonymous cart
           var anonymousCart = await CreateACart("123456789", null);
           Console.WriteLine($"anonymous cart created with Id {anonymousCart.Id}");
           
           anonymousCart = await AddProductToACartBySku(anonymousCart, "9812", 4);

           var customerSignInCommand = new LoginCustomerCommand(customer.Email, "password")
           {
               AnonymousCartId = anonymousCart.Id,
               AnonymousCartSignInMode = AnonymousCartSignInMode.MergeWithExistingCustomerCart
           };
           var result = await _client.ExecuteAsync(customerSignInCommand) as CustomerSignInResult;
           
           //LineItems of the anonymous cart will be copied to the customer’s active cart that has been modified most recently.
           var currentCustomerCart = result?.Cart;
           var lineItem = currentCustomerCart?.LineItems[0];
           Console.WriteLine($"SKU: {lineItem.Variant.Sku}, Quantity: {lineItem.Quantity}");
        }

        private async Task<Customer> CreateACustomer()
        {
            var result = await _client
                .Builder()
                .Customers()
                .SignUp(new CustomerDraft
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
            return await _client.Builder().Carts().Create(
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
            var action = new AddLineItemUpdateAction
            {
                Sku = sku,
                Quantity = quantity
            };


            return await _client.Builder().Carts().UpdateById(cart).AddAction(action).ExecuteAsync();
        }
    }
}