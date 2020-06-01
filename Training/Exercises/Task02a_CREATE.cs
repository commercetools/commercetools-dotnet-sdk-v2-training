using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// CREATE a customer
    /// CREATE a email verfification token
    /// Verfify customer
    /// </summary>
    public class Task02A : IExercise
    {
        private readonly IClient _client;

        public Task02A(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }

            this._client = clients.FirstOrDefault(c => c.Name == "Client"); // the default client
        }

        public async Task ExecuteAsync()
        {
            await ExecuteByCommands();
            //await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            //  CREATE a customer
            var signInResult = (CustomerSignInResult)
                await _client.ExecuteAsync(
                    new SignUpCustomerCommand(GetCustomerDraft())
                );
            var customer = signInResult.Customer;
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key}");
            
            //CREATE a email verfification token
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
            var signInResult = (CustomerSignInResult) await _client
                .Builder()
                .Customers()
                .SignUp(GetCustomerDraft())
                .ExecuteAsync();

            var customer = signInResult.Customer;
            
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key}");
            
            var verifiedCustomer = await _client
                .Builder()
                .Customers()
                .CreateTokenForEmailVerification("94cc6969-3be3-45e9-ac54-f070d4cab61c", 10, 1)
                .VerifyEmail()
                .ExecuteAsync();
            
            Console.WriteLine($"Is Email Verified:{verifiedCustomer.IsEmailVerified}");
        }

        private CustomerDraft GetCustomerDraft()
        {
            var rand = Settings.RandomInt();
            return new CustomerDraft
            {
                Email = $"michael_{rand}@example.com",
                Password = "password",
                Key = $"customer-michael-{rand}",
                FirstName = "michael",
                LastName = "hartwig"
            };
        }
    }
}