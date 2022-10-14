using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Channels;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Sdk.Api.Models.Payments;
using commercetools.Sdk.Api.Models.ShippingMethods;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Extensions;

namespace Training.Services
{
    public class CartService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public CartService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// GET a cart by Key
        /// </summary>
        /// <param name="cartKey"></param>
        /// <returns></returns>
        public async Task<ICart> GetCartByKey(string cartKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .WithKey(cartKey)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// GET a cart by id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task<ICart> GetCartById(string cartId)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .WithId(cartId)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// Create a new cart for a customer with default shipping address
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<ICart> CreateCart(ICustomer customer)
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
                InventoryMode = IInventoryMode.ReserveOnOrder
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .Post(cartDraft)
                .ExecuteAsync();
        }

        /// <summary>
        /// Create an anonymous cart
        /// </summary>
        /// <param name="anonymousId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ICart> CreateAnonymousCart(string anonymousId = null)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .Post(
                    new CartDraft
                    {
                        AnonymousId = anonymousId,
                        Currency = "EUR",
                        Country = "DE",
                        DeleteDaysAfterLastModification = 30
                    }
                )
                .ExecuteAsync();
        }

        /// <summary>
        /// Add Product to Cart by SKU and Supply Channel
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="channel"></param>
        /// <param name="skus"></param>
        /// <returns></returns>
        public async Task<ICart> AddProductToCartBySkusAndChannel(ICart cart, IChannel channel,
            params string[] skus)
        {
            var lineItemsToAddActions = new List<ICartUpdateAction>();
            foreach (var sku in skus)
            {
                lineItemsToAddActions.Add(new CartAddLineItemAction
                {
                    Sku = sku,
                    Quantity = 1,
                    SupplyChannel = new ChannelResourceIdentifier {Id = channel.Id}
                });
            }

            var cartUpdate = new CartUpdate
            {
                Version = cart.Version,
                Actions = lineItemsToAddActions
            };

            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .WithId(cart.Id)
                .Post(cartUpdate)
                .ExecuteAsync();
        }

        /// <summary>
        /// POST Add Discount Code update to the Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ICart> AddDiscountToCart(ICart cart, string code)
        {
            var update = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartAddDiscountCodeAction {Code = code}
                }
            };
            return await
                _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Carts()
                    .WithId(cart.Id)
                    .Post(update)
                    .ExecuteAsync();
        }

        //Recalculate a cart
        /// <summary>
        /// POST Recalculate update for the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<ICart> Recalculate(ICart cart)
        {
            var update = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartRecalculateAction()
                }
            };
            return await
                _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Carts()
                    .WithId(cart.Id)
                    .Post(update)
                    .ExecuteAsync();
        }

        /// <summary>
        /// POST Set ShippingMethod update for the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<ICart> SetShipping(ICart cart)
        {
            var shippingMethodsResult = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ShippingMethods()
                .MatchingCart()
                .Get()
                .WithCartId(cart.Id)
                .ExecuteAsync();

            var shippingMethod = shippingMethodsResult.Results.FirstOrDefault();
            var update = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartSetShippingMethodAction
                    {
                        ShippingMethod = new ShippingMethodResourceIdentifier
                        {
                            Id = shippingMethod?.Id
                        }
                    }
                }
            };

            return await
                _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Carts()
                    .WithId(cart.Id)
                    .Post(update)
                    .ExecuteAsync();
        }

        /// <summary>
        /// POST Add Payment to a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<ICart> AddPaymentToCart(ICart cart, IPayment payment)
        {
            var cartUpdate = new CartUpdate
            {
                Version = cart.Version,
                Actions = new List<ICartUpdateAction>
                {
                    new CartAddPaymentAction
                    {
                        Payment = new PaymentResourceIdentifier {Id = payment.Id}
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