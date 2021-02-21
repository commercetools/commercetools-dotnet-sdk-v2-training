using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task02A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
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
            
            //Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key} and Email Verified: {customer.IsEmailVerified}");
            
            //CREATE a email verfification token
            
            
            //Create ConfirmCustomerEmail

            //Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }
    }
}