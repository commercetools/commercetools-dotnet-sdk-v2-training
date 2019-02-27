using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;

namespace Training
{
    public class Exercise2 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        private static Random random = new Random();
        
        public Exercise2(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            var customerDraft = this.GetCustomerDraft();
            var signUpCustomerCommand = new SignUpCustomerCommand(customerDraft);
            var result =  _commercetoolsClient.ExecuteAsync(signUpCustomerCommand).Result as CustomerSignInResult;
            var customer = result?.Customer;
            if (customer != null)
            {
                Console.WriteLine(customer.Id);
                Console.WriteLine(customer.Version);
            }
        }


        private CustomerDraft GetCustomerDraft()
        {
            return new CustomerDraft
            {
                FirstName = "userName",
                LastName =  "test",
                Email = $"siaw{random.Next()}@asdf.com",
                Password = "adsf"
            };
        }
        
    }
}