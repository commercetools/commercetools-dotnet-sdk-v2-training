using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Orders;
using commercetools.Api.Models.States;
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
        private readonly string _channelKey = "berlin-supply-channel";
        private readonly string _customerKey = "customer-michele-george";
        private readonly string _discountCode = "BOGO";
        private readonly string _stateOrderedPackedKey = "mg-OrderPacked";
        private readonly string _productSku = "tulip-seed-sack";

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
            //Fetch a channel if your inventory mode will not be NONE
            //Get Channel By Key (not supported yet)
            var channelResult = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Channels()
                .Get()
                .WithWhere($"key=\"{_channelKey}\"")
                .ExecuteAsync();
            
            //check the result
            var channel = channelResult.Results.FirstOrDefault();
            Console.WriteLine($"Channel Id: {channel.Id}");

            //Fetch a state if you have a defined custom workflow
            var orderPacked = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .WithKey(_stateOrderedPackedKey)
                .Get()
                .ExecuteAsync();
            Console.WriteLine($" Custom state Id: {orderPacked.Id}");

            // TODO: Perform cart operations:
            //      Get Customer, create cart, add products, add inventory mode
            //      add discount codes, perform a recalculation
            // TODO: Convert cart into an order, set order status, set state in custom work flow

            var customer = await _customerService.GetCustomerByKey(_customerKey);

            //create a cart for a customer
            var cart = await _cartService.CreateCart(customer);

            Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");
            
            // add items to the cart
            cart = await
                _cartService.AddProductToCartBySkusAndChannel(cart, channel, _productSku, _productSku,_productSku);

            //Add discount coupon code to the cart
            cart = await _cartService.AddDiscountToCart(cart, _discountCode);
            cart = await _cartService.Recalculate(cart);

            //Add default shipping to the cart
            cart = await _cartService.SetShipping(cart);
            
            var payment = await _paymentService.CreatePayment(cart, "WIRECARD", "CREDIT_CARD", $"wire{Settings.RandomInt()}");
            Console.WriteLine($"Payment Created with Id: {payment.Id}");

            payment = await _paymentService.AddTransactionToPayment(cart, payment, $"pay{Settings.RandomInt()}");

            cart = await _cartService.AddPaymentToCart( cart, payment );

            var order = await _orderService.CreateOrder(cart);
            Console.WriteLine($"Order Created with Id: {order.Id}");
            
            order = await _orderService.ChangeOrderState(order, IOrderState.Complete);
            Console.WriteLine($"Order state changed to: {order.OrderState.Value}");
            
            order = await _orderService.ChangeWorkflowState(order,
                new StateResourceIdentifier {Key = orderPacked.Key});
            
            Console.WriteLine($"Order Workflow State changed to: {order.State?.Obj?.Name["en"]}");
        }
        
    }
}