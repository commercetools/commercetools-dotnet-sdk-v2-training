using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Channels;
using commercetools.Sdk.Api.Models.Orders;
using commercetools.Sdk.Api.Models.States;
using commercetools.Sdk.Api.Models.Subscriptions;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

using Training.Services;

namespace Training
{
    /// <summary>
    /// Create a cart for a customer, add a product to it, create an order from the cart and change the order state.
    /// </summary>
    public class Task04B : IExercise
    {
        private readonly IClient _client;
        private const string _channelKey = "berlin-store-channel";
        private const string _customerKey = "nd-customer";
        private const string _discountCode = "BOGOOFFER";
        private const string _stateOrderedPackedKey = "ndOrderPacked";
        private const string _productSku = "tulip-seed-box";

        private readonly CustomerService _customerService;
        private readonly CartService _cartService;
        private readonly PaymentService _paymentService;
        private readonly OrderService _orderService;


        public Task04B(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
            _cartService = new CartService(_client, Settings.ProjectKey);
            _paymentService = new PaymentService(_client, Settings.ProjectKey);
            _orderService = new OrderService(_client, Settings.ProjectKey);

        }

        public async Task ExecuteAsync()
        {
            // GET customer
            var customer = await _customerService.GetCustomerByKey(_customerKey);

            // CREATE a cart for the customer
            var cart = await _cartService.CreateCart(customer);
            //var cart = await _cartService.GetCartById("5ee8505e-9325-4e45-9296-3211ae93713e");

            Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");

            // GET a channel if your inventory mode will not be NONE

            // ADD items to the cart
            cart = await _cartService.AddProductsToCartBySkusAndChannel(
                cart,
                _channelKey,
                _productSku
                );

            // ADD discount coupon code to the cart
            //cart = await _cartService.AddDiscountToCart(cart, _discountCode);

            // RECALCULATE the cart

            // ADD default shipping to the cart
            cart = await _cartService.SetShipping(cart);

            // CREATE a payment 
            //var payment = await _paymentService.CreatePayment(cart,
            //    "Adyen",
            //    "Credit",
            //    "ADYEN_002"
            //    );
            // Console.WriteLine($"Payment Created with Id: {payment.Id}");

            //// ADD transaction to the payment
            //payment = await _paymentService.AddTransactionToPayment(
            //    cart,
            //    payment,
            //    "res002");
            //// ADD payment to the cart
            //cart = await _cartService.AddPaymentToCart(cart, payment);

            // CREATE order
            var order = await _orderService.CreateOrder(cart);

            Console.WriteLine($"Order Created with order number: {order.Id}");

            order = await _orderService.ChangeOrderState(order, IOrderState.Confirmed);
            Console.WriteLine($"Order state changed to: {order.OrderState.Value}");

            order = await _orderService.ChangeWorkflowState(order, _stateOrderedPackedKey);
            Console.WriteLine($"Order Workflow State changed to: {order.State?.Obj?.Name["en"]}");
        }

    }
}