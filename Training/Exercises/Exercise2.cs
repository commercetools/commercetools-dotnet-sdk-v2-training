using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;

namespace Training
{
    public class Exercise2 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise2(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            var createCommand = new CreateCommand<Customer>(this.GetCustomerDraft());
            var customer =  _commercetoolsClient.ExecuteAsync(createCommand).Result;
            Console.WriteLine(customer.Id);
            Console.WriteLine(customer.Version);
        }


        private CustomerDraft GetCustomerDraft()
        {
            return new CustomerDraft
            {
                FirstName = "userName",
                LastName =  "test",
                Email = "siaw@asdf.com",
                Password = "adsf"
            };
        }
        
    }
}