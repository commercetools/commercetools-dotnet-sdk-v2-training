using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Channels;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Customers;
using commercetools.Api.Models.Orders;
using commercetools.Api.Models.Payments;
using commercetools.Api.Models.ShippingMethods;
using commercetools.Api.Models.States;
using commercetools.Base.Client;
using Training.Extensions;

namespace Training
{
    /// <summary>
    /// Create a cart for a customer, add a product to it, create an order from the cart and change the order state.
    /// </summary>
    public class Task04B : IExercise
    {
        private readonly IClient _client;
        private readonly string _channelKey = "sunrise-store-paris";
        private readonly string _customerKey = "ronnieWood";
        private readonly string _discountCode = "SUMMER";
        private readonly string _stateOrderedPackedKey = "OrderPacked";
        private readonly string _productSku = "A0E200000001WG3";

        public Task04B(IClient client)
        {
            this._client = client;
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


            //Fetch a state if you have a defined custom workflow
            var orderPacked = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States().WithKey(_stateOrderedPackedKey).Get().ExecuteAsync();

            // TODO: Perform cart operations:
            //      Get Customer, create cart, add products, add inventory mode
            //      add discount codes, perform a recalculation
            // TODO: Convert cart into an order, set order status, set state in custom work flow

            var customer = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers().WithKey(_customerKey).Get().ExecuteAsync();

            //create a cart for a customer
            var cart = await CreateCart(customer);

            Console.WriteLine($"Cart {cart.Id} for customer: {cart.CustomerId}");
            

            cart = await
                AddProductToCartBySkusAndChannel(cart, channel, _productSku, _productSku,_productSku);

            cart = await AddDiscountToCart(cart, _discountCode);
            cart = await Recalculate(cart);
            cart = await SetShipping(cart);
            
            cart = await CreatePaymentAndAddToCart(cart, "WIRECARD", "CREDIT_CARD", $"wire{Settings.RandomInt()}", $"pay{Settings.RandomInt()}");

            var order = await CreateOrder(cart);
            Console.WriteLine($"Order Created with Id: {order.Id}");
            
            order = await ChangeOrderState(order, IOrderState.Complete);
            Console.WriteLine($"Order state changed to: {order.OrderState.Value}");
            
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
        private async Task<Cart> CreateCart(Customer customer)
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
                .Carts().Post(cartDraft).ExecuteAsync();
        }

        /// <summary>
        /// Add Product to Cart by SKU and Supply Channel
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="channel"></param>
        /// <param name="skus"></param>
        /// <returns></returns>
        private async Task<Cart> AddProductToCartBySkusAndChannel(Cart cart, IChannel channel,
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
        /// Add DiscountCode to Cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<Cart> AddDiscountToCart(Cart cart, string code)
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
        private async Task<Cart> Recalculate(Cart cart)
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
        /// Set ShippingMethod for a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<Cart> SetShipping(Cart cart)
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
            var paymentMethodInfo = new PaymentMethodInfo
            {
                Method = pspMethod, // PSP Provider Method: CreditCard
                PaymentInterface = pspName, // PSP Provider Name: WireCard, ....
            };
            var paymentDraft = new PaymentDraft
            {
                PaymentMethodInfo = paymentMethodInfo,
                InterfaceId = interfaceId,
                AmountPlanned = new CentPrecisionMoneyDraft
                {
                    CentAmount = cart.TotalPrice.CentAmount,
                    CurrencyCode = cart.TotalPrice.CurrencyCode
                }
            };

            var payment = await _client
                .WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .Post(paymentDraft).ExecuteAsync();

            Console.WriteLine($"Payment Created with Id {payment.Id}");

            //Create the transaction
            var transactionDraft = new TransactionDraft
            {
                Type = ITransactionType.Charge,
                Amount = new CentPrecisionMoneyDraft
                {
                    CentAmount = cart.TotalPrice.CentAmount,
                    CurrencyCode = cart.TotalPrice.CurrencyCode
                },
                InteractionId = interactionId,
                Timestamp = DateTime.Now,
            };

            var paymentUpdate = new PaymentUpdate
            {
                Version = payment.Version,
                Actions = new List<IPaymentUpdateAction>
                {
                    new PaymentAddTransactionAction
                    {
                        Transaction = transactionDraft
                    }
                }
            };

            //payment with a transaction
            payment = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .WithId(payment.Id)
                .Post(paymentUpdate)
                .ExecuteAsync();

            paymentUpdate = new PaymentUpdate
            {
                Version = payment.Version,
                Actions = new List<IPaymentUpdateAction>
                {
                    new PaymentSetStatusInterfaceCodeAction {InterfaceCode = "SUCCESS"},
                    new PaymentSetStatusInterfaceTextAction {InterfaceText = "We got the money."},
                }
            };

            //set interface code and text
            payment = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .WithId(payment.Id)
                .Post(paymentUpdate)
                .ExecuteAsync();

            //then add ref of payment to the cart

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

        /// <summary>
        /// Create an Order From the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private async Task<Order> CreateOrder(Cart cart)
        {
            var orderFromCartDraft = new OrderFromCartDraft
            {
                Id = cart.Id,
                Version = cart.Version,
            };
            return await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Orders()
                .Post(orderFromCartDraft).ExecuteAsync();
        }


        /// <summary>
        /// Change Order State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task<Order> ChangeOrderState(Order order, IOrderState state)
        {
            var orderUpdate = new OrderUpdate
            {
                Version = order.Version,
                Actions = new List<IOrderUpdateAction>
                {
                    new OrderChangeOrderStateAction {OrderState = state}
                }
            };

            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithId(order.Id)
                .Post(orderUpdate)
                .ExecuteAsync();
        }

        private async Task<Order> ChangeWorkflowState(Order order, IStateResourceIdentifier state)
        {
            var orderUpdate = new OrderUpdate
            {
                Version = order.Version,
                Actions = new List<IOrderUpdateAction>
                {
                    new OrderTransitionStateAction() {State = state}
                }
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithId(order.Id)
                .Post(orderUpdate)
                .WithExpand("state")
                .ExecuteAsync();
        }

        #endregion
    }
}