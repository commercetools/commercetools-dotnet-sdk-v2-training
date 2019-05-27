using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;

namespace Training
{
    /// <summary>
    /// Verify Customer Email Exercise
    /// </summary>
    public class Exercise08A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise08A(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //Get Customer By ID
            Customer customer = await _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Customer>(new Guid(Settings.CUSTOMERID)));

            //Create token for verifying the customer's email
            var customerTokenResult = await _commercetoolsClient.ExecuteAsync(new CreateTokenForCustomerEmailVerificationCommand(customer.Id, 10, customer.Version));

            if (customerTokenResult is CustomerToken customerToken)
            {
                //Create VerifyCustomerEmailCommand and Execute it
                Customer retrievedCustomer = await _commercetoolsClient
                    .ExecuteAsync(new VerifyCustomerEmailCommand(customerToken.Value, customer.Version));

                Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
            }
        }

    }
}
