using System;
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
            throw new NotImplementedException();
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
        public async Task<ICart> AddProductsToCartBySkusAndChannel(ICart cart, IChannel channel,
            params string[] skus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST Add Discount Code update to the Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ICart> AddDiscountToCart(ICart cart, string code)
        {
            throw new NotImplementedException();
        }

        //Recalculate a cart
        /// <summary>
        /// POST Recalculate update for the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<ICart> Recalculate(ICart cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST Set ShippingMethod update for the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<ICart> SetShipping(ICart cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST Add Payment to a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<ICart> AddPaymentToCart(ICart cart, IPayment payment)
        {
            throw new NotImplementedException();
        }
        
    }
}