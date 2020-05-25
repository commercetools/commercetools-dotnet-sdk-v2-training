using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// Verify Customer Email Exercise
    /// </summary>
    public class Exercise08A : IExercise
    {
        private readonly IClient _client;

        public Exercise08A(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //await ExecuteByCommands();
            await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            //Get Customer By ID
            var customer = await _client.ExecuteAsync(
                new GetByIdCommand<Customer>(Settings.CUSTOMERID));

            //Create token for verifying the customer's email
            var customerTokenResult = await _client.ExecuteAsync(
                new CreateTokenForCustomerEmailVerificationCommand(customer.Id, 10, customer.Version));

            if (customerTokenResult is CustomerToken customerToken)
            {
                //Create VerifyCustomerEmailCommand and Execute it
                Customer retrievedCustomer = await _client
                    .ExecuteAsync(new VerifyCustomerEmailCommand(customerToken.Value, customer.Version));

                Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
            }
        }
        
        private async Task ExecuteByBuilder()
        {
            var verifiedCustomer = await _client
                .Builder()
                .Customers()
                .CreateTokenForEmailVerification("94cc6969-3be3-45e9-ac54-f070d4cab61c", 10, 1)
                .VerifyEmail()
                .ExecuteAsync();
            
            Console.WriteLine($"Is Email Verified:{verifiedCustomer.IsEmailVerified}");

        }

    }
}
