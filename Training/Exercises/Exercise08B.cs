using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;
using System.Threading.Tasks;
using commercetools.Sdk.Domain;

namespace Training
{
    /// <summary>
    /// Get Customer By ID and Verify Email Exercise - async chaining tasks
    /// </summary>
    public class Exercise08B : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise08B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            var customerByIdTask =
                _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Customer>(new Guid(Settings.CUSTOMERID)));

            // Verify Customer Email as Chaining multiple tasks using ContinueWith
            var retrievedCustomer = await
                customerByIdTask
                    .ContinueWith(
                        customerTask => CreateTokenForCustomerEmailVerificationTask(customerTask.Result),
                        TaskContinuationOptions.OnlyOnRanToCompletion)
                    .Unwrap() // to return Task<Token<Customer>> instead of Task<Task<Token<Customer>>>
                    .ContinueWith(
                        customerTokenTask => VerifyCustomerEmailTask(customerTokenTask.Result as CustomerToken,
                            customerByIdTask.Result.Version),
                        TaskContinuationOptions.OnlyOnRanToCompletion)
                    .Unwrap();

            Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }

        /// <summary>
        /// Return Task for Create Token for Customer Email Verification
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private Task<Token<Customer>> CreateTokenForCustomerEmailVerificationTask(Customer customer)
        {
            Console.WriteLine($"CustomerById Task finished, here is the customer last name: {customer.LastName}");
            Console.WriteLine($"Starting CreateTokenForCustomerEmailVerification Task");
            return _commercetoolsClient.ExecuteAsync(
                new CreateTokenForCustomerEmailVerificationCommand(customer.Id, 10, customer.Version));
        }

        /// <summary>
        /// Return Task for Customer Email Verification
        /// </summary>
        /// <param name="customerToken"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private Task<Customer> VerifyCustomerEmailTask(CustomerToken customerToken, int version)
        {
            Console.WriteLine(
                $"CreateTokenForCustomerEmailVerification Task finished, here is the token: {customerToken.Value}");
            Console.WriteLine($"Starting verifying customer email task");
            return _commercetoolsClient.ExecuteAsync(
                new VerifyCustomerEmailCommand(customerToken.Value, version));
        }
    }
}