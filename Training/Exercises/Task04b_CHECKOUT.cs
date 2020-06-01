using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Channels;
using commercetools.Sdk.Domain.Common;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Orders;
using commercetools.Sdk.Domain.Orders.UpdateActions;
using commercetools.Sdk.Domain.Payments;
using commercetools.Sdk.Domain.Payments.UpdateActions;
using commercetools.Sdk.Domain.States;
using commercetools.Sdk.HttpApi.CommandBuilders;
using Training.Extensions;
using AddPaymentUpdateAction = commercetools.Sdk.Domain.Carts.UpdateActions.AddPaymentUpdateAction;
using TransitionStateUpdateAction = commercetools.Sdk.Domain.Orders.UpdateActions.TransitionStateUpdateAction;

namespace Training
{
    /// <summary>
    /// Create a cart for a customer, add a product to it, create an order from the cart and change the order state.
    /// </summary>
    public class Task04B : IExercise
    {
        private readonly IClient _client;

        public Task04B(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //Fetch a channel if your inventory mode will not be NONE
            //Get Channel By Key (not supported yet)
           
            //check the result
            Channel channel = null;


            //Fetch a state if you have a defined custom workflow
            State orderPacked = null;

            // TODO: Perform cart operations:
            //      Get Customer, create cart, add products, add inventory mode
            //      add discount codes, perform a recalculation
            // TODO: Convert cart into an order, set order status, set state in custom work flow


            // Get the customer By Key
            Customer customer = null;

            //create a cart for a customer
            var cart = await CreateCart(customer);

            Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");

            //AddProductToCartBySkusAndChannel
            cart = await
                AddProductToCartBySkusAndChannel(cart, channel, "9812", "9812", "9812");

            //AddDiscountToCart
            cart = await AddDiscountToCart(cart, "SUMMER");
            
            //Recalculate it
            cart = await Recalculate(cart);
            
            //SetShipping for the cart
            cart = await SetShipping(cart);

            //CreatePaymentAndAddToCart
            cart = await CreatePaymentAndAddToCart(cart, "WIRECARD", "CREDIT_CARD", "wire73638", "pay82628");

            //CreateOrder
            var order = await CreateOrder(cart);
            order = await ChangeOrderState(order, OrderState.Complete);
            order = await ChangeWorkflowState(order, orderPacked.ToReference());
        }


        #region HelperFunctions

        /// <summary>
        /// Create Cart for a customer with DefaultShipping Address
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private async Task<Cart> CreateCart(Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add Product to Cart by SKU and Supply Channel
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="channel"></param>
        /// <param name="skus"></param>
        /// <returns></returns>
        private async Task<Cart> AddProductToCartBySkusAndChannel(Cart cart, Channel channel, params string[] skus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add DiscountCode to Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<Cart> AddDiscountToCart(Cart cart, string code)
        {
            throw new NotImplementedException();
        }

        //Recalculate a cart
        private async Task<Cart> Recalculate(Cart cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ShippingMethod for a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<Cart> SetShipping(Cart cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create A Payment and Add it to the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="pspName"></param>
        /// <param name="pspMethod"></param>
        /// <param name="interfaceId"></param>
        /// <param name="interactionId"></param>
        /// <returns></returns>
        private async Task<Cart> CreatePaymentAndAddToCart(Cart cart, string pspName, string pspMethod,
            string interfaceId, string interactionId)
        {
            // we create payment object
         

            //Create the transaction
         
            //payment with a transaction
         
            //set interface code and text
         
            
            //then add ref of payment to the cart
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create an Order From the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<Order> CreateOrder(Cart cart)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Change Order State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task<Order> ChangeOrderState(Order order, OrderState state)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ChangeWorkflowState(Order order, IReference<State> state)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}