using System;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Customers;
using commercetools.Base.Client;

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

        public Task02A(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            var rand = Settings.RandomInt();
            
            //  Create customer draft
            var customerDraft = new CustomerDraft
            {
                Email = $"michele_{rand}@example.com",
                Password = "password",
                Key = $"customer-michele-{rand}",
                FirstName = "michele",
                LastName = "george"
            };
            
            //  SignUp a customer
            var customerSignInResult = await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();
            var customer = customerSignInResult.Customer;
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key} and Email Verified: {customer.IsEmailVerified}");
            
            //CREATE a email verfification token
            var customerTokenResult = await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .EmailToken()
                .Post(new CustomerCreateEmailToken
                {
                    Id = customer.Id,
                    Version = customer.Version,
                    TtlMinutes = 10
                }).ExecuteAsync();
            
            //Create ConfirmCustomerEmail
            await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .EmailConfirm()
                .Post(new CustomerEmailVerify
                {
                    TokenValue = customerTokenResult.Value
                }).ExecuteAsync();

            var retrievedCustomer = await _client
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .WithId(customer.Id)
                .Get().ExecuteAsync();
            
            Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }
    }
}