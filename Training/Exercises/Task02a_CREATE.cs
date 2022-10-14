using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Base.Client;
using Training.Services;

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
        private readonly CustomerService _customerService;
        private const string _customerKey = "";

        public Task02A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            // CREATE customer draft
            var customerDraft = new CustomerDraft
            {
                Email = "michele@example.com",
                Password = "password",
                Key = _customerKey,
                FirstName = "",
                LastName = "",
                Addresses = new List<IBaseAddress>{
                        new AddressDraft {
                            Country = "DE",
                            Key = _customerKey +"-home"
                    }
                },
                DefaultShippingAddress = 0,
                DefaultBillingAddress = 0
            };
            
            // TODO: SIGNUP a customer
            
            //Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key} and Email Verified: {customer.IsEmailVerified}");
            
            // TODO: CREATE a email verfification token
            
            
            // TODO: CONFIRM CustomerEmail

            //Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }
    }
}