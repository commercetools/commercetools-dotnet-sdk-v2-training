using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Carts.UpdateActions;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Orders;
using commercetools.Sdk.Domain.Payments;
using commercetools.Sdk.Domain.Payments.UpdateActions;
using Type = commercetools.Sdk.Domain.Type;

namespace Training
{
    public class Exercise53B : IExercise
    {
        private readonly IClient _client;

        public Exercise53B(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            // Add a unique number for different tryouts
            var unique = Settings.RandomInt();

            // Step -1: Create a cart and add a product to it
            // (Optional) Get an old cart from the customer
            var customer =
                await _client.ExecuteAsync(new GetByKeyCommand<Customer>(Settings.CUSTOMERKEY));

            var cart = await CreateCart(customer);

            // Step 0: Customer decides to go for payment
            // Generation of a unique id.
            // Every Payment Provider requires a unique id. (Often called orderID)
            // Problem: We often create order AFTER payment.
            // Problem: Some customer play with multiple payment options in different tabs.
            // Possible solution: Create a unique order id per payment try like order8373465-1, order8373465-2 .. and never show the last part (-1, -2) to the customer
            var orderId = $"Order-{unique}";


            // Step 1: Storefront displays payment options, customer chooses
            // Storefront talks directly to psp about the different payment methods and displays the options
            // Or the store renders hard coded payment methods
            // Customer decides for payment.

            //
            // Step 2: Storefront talks to psp, we create payment object
            //
            var paymentMethodInfo = new PaymentMethodInfo
            {
                Method = "CREDIT_CARD",
                PaymentInterface = "WIRECARD",// PSP Provider: WireCard, ....
                Name = new LocalizedString {{"en", "WIRECARD-Method"}}
            };
            //get totalAmount of cart
            var totalAmount = cart.TotalPrice;
            var paymentDraft = new PaymentDraft
            {
                PaymentMethodInfo = paymentMethodInfo,
                AmountPlanned = totalAmount
            };

            var payment = await _client.ExecuteAsync(new CreateCommand<Payment>(paymentDraft));

            // Step 3
            // Payment is done via Storefront or API extension
            // we get the information
            var paymentServiceID = $"payment{unique}";
            var paymentServiceURL = "http://superpay";



            // Step 4
            // Store payments' unique id on the payment object
            // It is the only link between payment on the psp and our payment object
            var updatePaymentActions = new List<UpdateAction<Payment>>();
            var setInterfaceIdUpdateAction = new SetInterfaceIdUpdateAction()
            {
                InterfaceId = paymentServiceID
            };
            updatePaymentActions.Add(setInterfaceIdUpdateAction);

            var paymentWithId = await _client.ExecuteAsync(new UpdateByIdCommand<Payment>(payment, updatePaymentActions));

            // Step 4b
            // store other payment related info on the payment object
            // urls, connection info,...


            // Step 4c
            // Store the payment info on the cart, otherwise the cart has no connection to that payment
            var addPaymentUpdateAction = new AddPaymentUpdateAction()
            {
                Payment = new Reference<Payment>() {Id = payment.Id}
            };

            var updateCartActions = new List<UpdateAction<Cart>> {addPaymentUpdateAction};

            var cartWithPayment = await _client.ExecuteAsync(new UpdateByIdCommand<Cart>(cart, updateCartActions));

            // Step 5: Customer keys in CreditCard info etc..
            // we wait
            var paymentServiceCreditCardUsed = "1234-XXXXXXXXXXXXXXX";


            // Step 6: Storefront creates a transaction with PSP
            //
            var interactionId = "charged" + unique;
            var transactionInfo = "Everything went well.";

            // Step 7
            //
            var transactionDraft = new TransactionDraft
            {
                Type = TransactionType.Charge,
                Amount = totalAmount,
                InteractionId = interactionId,
                Timestamp = DateTime.Now
            };

            updatePaymentActions.Clear();
            var addTransactionUpdateAction = new AddTransactionUpdateAction
            {
                Transaction = transactionDraft
            };
            updatePaymentActions.Add(addTransactionUpdateAction);

            var paymentWithCharge = await _client.ExecuteAsync(new UpdateByIdCommand<Payment>(paymentWithId, updatePaymentActions));

            // Step 8) Logging all talking to psp
            // add all InterfaceInteractions
            // very different per PSP, often a lot of custom fields
            var wireCardCustomType = await _client.ExecuteAsync(new GetByKeyCommand<Type>("WirecardPaymentTran-Key"));

            var addInterfaceInteractionAction = new AddInterfaceInteractionUpdateAction
            {
                Type = new ResourceIdentifier
                {
                    Id = wireCardCustomType.Id
                },
                Fields = new Fields {{"transactionWireCard", transactionInfo}}
            };
            updatePaymentActions.Clear();
            updatePaymentActions.Add(addInterfaceInteractionAction);
            var updatedPayment = await _client.ExecuteAsync(new UpdateByIdCommand<Payment>(paymentWithCharge, updatePaymentActions));


            // Often payment means to react to asynchronous incoming notifications.
            // Log all those notifications as interfaceInteractions.
            // b) Plus Status vom Payment evtl. ändern. Wie interpretiert man das hängt vom UseCase ab. Man kann für ein payment PENDING bekommen, dann später SUCCESS (oder FAILURE).
            //
            // Step 9: We get a success asychronously.
            var interfaceCode = "SUCCESS";
            var interfaceText = "We got the money.";

            updatePaymentActions.Clear();
            var updatePaymentStatusCodeAction = new SetStatusInterfaceCodeUpdateAction { InterfaceCode = interfaceCode };
            var updatePaymentStatusTextAction = new SetStatusInterfaceTextUpdateAction { InterfaceText = interfaceText };
            updatePaymentActions.Add(updatePaymentStatusCodeAction);
            updatePaymentActions.Add(updatePaymentStatusTextAction);

            var successUpdatedPayment = await _client.ExecuteAsync(new UpdateByIdCommand<Payment>(updatedPayment, updatePaymentActions));

            //Step 10) Create order from the cart
            //
            var orderFromCartDraft = new OrderFromCartDraft
            {
                Id = cartWithPayment.Id,
                Version = cartWithPayment.Version,
                OrderNumber = orderId,
                OrderState = OrderState.Complete
            };

            var order = await _client.ExecuteAsync(new CreateCommand<Order>(orderFromCartDraft));
            Console.WriteLine($"Order Created with Number: {order.OrderNumber}");
        }

        /// <summary>
        /// Create A Cart with lineItem for customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private Task<Cart> CreateCart(Customer customer)
        {
            var cartDraft = GetCartDraftWithLineItem(customer);
            var createCartTask = _client.ExecuteAsync(new CreateCommand<Cart>(cartDraft));
            return createCartTask;
        }

        private CartDraft GetCartDraftWithLineItem(Customer customer)
        {
            CartDraft cartDraft = new CartDraft();
            cartDraft.Currency = "EUR";
            cartDraft.CustomerId = customer.Id;
            cartDraft.ShippingAddress = new Address()
            {
                Country = "DE"
            };
            cartDraft.DeleteDaysAfterLastModification = 1;
            cartDraft.LineItems = new List<LineItemDraft>
            {
                new LineItemDraft
                {
                    Sku = Settings.PRODUCTVARIANTSKU,
                    Quantity = 6
                }
            };
            return cartDraft;
        }
    }
}
