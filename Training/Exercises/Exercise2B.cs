using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;

namespace Training
{
    /// <summary>
    /// Verify Email Exercise
    /// </summary>
    public class Exercise2B : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise2B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            VerifyCustomerEmail();
        }

        private void VerifyCustomerEmail()
        {
            //Get Customer By ID
            Customer customer = _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Customer>(new Guid(Settings.CUSTOMERID))).Result;
            
            //Create token for verifying the customer's email
            CustomerToken customerToken = _commercetoolsClient.ExecuteAsync(new CreateTokenForCustomerEmailVerificationCommand(customer.Id, 10, customer.Version)).Result as CustomerToken;
            
            //Create VerifyCustomerEmailCommand and Execute it
            Customer retrievedCustomer = _commercetoolsClient.ExecuteAsync(new VerifyCustomerEmailCommand(customerToken.Value, customer.Version)).Result;
            
            Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }
        
    }
}