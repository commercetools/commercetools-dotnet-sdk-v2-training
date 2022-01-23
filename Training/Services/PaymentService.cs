using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Payments;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;


namespace Training.Services
{
    public class PaymentService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public PaymentService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
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
        public async Task<IPayment> CreatePayment(ICart cart, string pspName, string pspMethod,
            string interfaceId)
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

            return await _client
                .WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .Post(paymentDraft).ExecuteAsync();
        }
        public async Task<IPayment> AddTransactionToPayment(ICart cart, IPayment payment, string interactionId)
        {
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
            var updatedPayment =  await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .WithId(payment.Id)
                .Post(paymentUpdate)
                .ExecuteAsync();

            var successUpdate = new PaymentUpdate
            {
                Version = updatedPayment.Version,
                Actions = new List<IPaymentUpdateAction>
                {
                    new PaymentSetStatusInterfaceCodeAction {InterfaceCode = "SUCCESS"},
                    new PaymentSetStatusInterfaceTextAction {InterfaceText = "We got the money."},
                }
            };

            //set interface code and text
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Payments()
                .WithId(payment.Id)
                .Post(successUpdate)
                .ExecuteAsync();
        }        
    }
}