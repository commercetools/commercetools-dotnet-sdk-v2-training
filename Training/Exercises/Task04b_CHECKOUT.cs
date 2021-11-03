using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Channels;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Customers;
using commercetools.Api.Models.Orders;
using commercetools.Api.Models.Payments;
using commercetools.Api.Models.ShippingMethods;
using commercetools.Api.Models.States;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Extensions;

namespace Training
{
    /// <summary>
    /// Create a cart for a customer, add a product to it, create an order from the cart and change the order state.
    /// </summary>
    public class Task04B : IExercise
    {
        private readonly IClient _client;
        private readonly string _channelKey = "";
        private readonly string _customerKey = "";
        private readonly string _discountCode = "SUMMER";
        private readonly string _stateOrderedPackedKey = "OrderPacked";
        private readonly string _productSku = "";

        public Task04B(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            //Fetch a channel if your inventory mode will not be NONE
            
            //check the result
            Channel channel = null;


            //Fetch orderPacked state if you have a defined custom workflow
            State orderPacked = null;
            
            // Get Customer by Key
            Customer customer = null;

            //create a cart for a customer
            var cart = await CreateCart(customer);

            Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");
            
            //AddProductToCartBySkusAndChannel
            cart = await
                AddProductToCartBySkusAndChannel(cart, channel, _productSku, _productSku,_productSku);

            //AddDiscountToCart
            cart = await AddDiscountToCart(cart, _discountCode);
            //Recalculate it
            cart = await Recalculate(cart);
            //SetShipping for the cart
            cart = await SetShipping(cart);
            
            //CreatePaymentAndAddToCart
            cart = await CreatePaymentAndAddToCart(cart, "WIRECARD", "CREDIT_CARD", $"wire{Settings.RandomInt()}", $"pay{Settings.RandomInt()}");

            //CreateOrder
            var order = await CreateOrder(cart);
            Console.WriteLine($"Order Created with Id: {order.Id}");
            
            //Change Order State
            order = await ChangeOrderState(order, IOrderState.Complete);
            Console.WriteLine($"Order state changed to: {order.OrderState.Value}");
            
            //Change Workflow State
            order = await ChangeWorkflowState(order,
                new StateResourceIdentifier {Key = orderPacked.Key});
            
            Console.WriteLine($"Order Workflow State changed to: {order.State?.Obj?.Name["en"]}");
        }


        #region HelperFunctions

        /// <summary>
        /// Create Cart for a customer with DefaultShipping Address
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private async Task<ICart> CreateCart(ICustomer customer)
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
        private async Task<ICart> AddProductToCartBySkusAndChannel(ICart cart, IChannel channel,
            params string[] skus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add DiscountCode to Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<ICart> AddDiscountToCart(ICart cart, string code)
        {
            throw new NotImplementedException();
        }

        //Recalculate a cart
        private async Task<ICart> Recalculate(ICart cart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set ShippingMethod for a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<ICart> SetShipping(ICart cart)
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
        private async Task<ICart> CreatePaymentAndAddToCart(ICart cart, string pspName, string pspMethod,
            string interfaceId, string interactionId)
        {
            // we create payment object
            // Create PaymentMethodInfo, PaymentDraft then Payment
            

            //Create the transactionDraft, Update Payment to Transaction to it

            
            //set interface code and text
            

            //then add ref of payment to the cart
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create an Order From the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<IOrder> CreateOrder(ICart cart)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Change Order State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task<IOrder> ChangeOrderState(IOrder order, IOrderState state)
        {
            throw new NotImplementedException();
        }

        private async Task<IOrder> ChangeWorkflowState(IOrder order, IStateResourceIdentifier state)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}