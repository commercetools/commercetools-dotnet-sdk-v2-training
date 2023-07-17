using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Anonymous Cart Merging
    /// </summary>
    public class Task04C : IExercise
    {
        private readonly IClient _client;
        private const string _channelKey = "";
        private const string _customerKey = "";
        private const string _customerPassword = "password";
        private readonly CustomerService _customerService;
        private readonly CartService _cartService;
        
        public Task04C(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
            _cartService = new CartService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
           // Get the customer
           var customer = await _customerService.GetCustomerByKey(_customerKey);
           
           //Create Cart for this customer

           var cart = await _cartService.CreateCart(customer);
           Console.WriteLine($"cart for customer created with Id {cart.Id}");
           
           //Add Product to cart
           cart = await _cartService.AddProductsToCartBySkusAndChannel(cart, _channelKey, "tulip-seed-package", "tulip-seed-sack");

           // Create Anonymous cart
           var anonymousCart = await _cartService.CreateAnonymousCart("mg123456789");
           Console.WriteLine($"anonymous cart created with Id {anonymousCart.Id}");
           
           //Add Product to the Anonymous cart
           anonymousCart = await _cartService.AddProductsToCartBySkusAndChannel(anonymousCart, _channelKey, "tulip-seed-package", "tulip-seed-package");
           
           // TODO: Decide on a merging strategy
           var result = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
               .Login()
               .Post(new CustomerSignin
               {
                   AnonymousCart = new CartResourceIdentifier {
                       Id = anonymousCart.Id
                    },
                   AnonymousCartSignInMode = IAnonymousCartSignInMode.UseAsNewActiveCustomerCart,
                   Email = customer.Email,
                   Password = _customerPassword

               }).ExecuteAsync();
           
           //LineItems of the anonymous cart will be copied to the customer’s active cart that has been modified most recently.
           var currentCustomerCart = result?.Cart as Cart;
           if (currentCustomerCart != null)
           {
               Console.WriteLine($"Final Cart Id:{currentCustomerCart.Id}");
               foreach (var lineItem in currentCustomerCart.LineItems)
               {
                   Console.WriteLine($"SKU: {lineItem.Variant.Sku}, Quantity: {lineItem.Quantity}");
               }
           }
        }
    }
}