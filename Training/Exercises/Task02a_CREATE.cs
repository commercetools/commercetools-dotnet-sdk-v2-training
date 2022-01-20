using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Customers;
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
        private readonly string _customerKey = "michele-george";

        public Task02A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var rand = Settings.RandomInt();
            
            //  Create customer draft
            var customerDraft = new CustomerDraft
            {
                Email = "michele3@example.com",
                Password = "password",
                Key = _customerKey,
                FirstName = "michele",
                LastName = "george",
                Addresses = new List<IBaseAddress>{
                        new AddressDraft {
                            Country = "DE",
                    }
                },
                DefaultShippingAddress = 0,
                DefaultBillingAddress = 0
            };
            
            //  SignUp a customer
            var customer = await _customerService.CreateCustomer(customerDraft);
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key} and Email Verified: {customer.IsEmailVerified}");

            // fetch a customer by the key
            // var customer = await _customerService.GetCustomerByKey(_customerKey);    
            // Console.WriteLine($"Customer Id : {customer.Id} and Key : {customer.Key} and Email Verified: {customer.IsEmailVerified}");
            
            // CREATE a email verfification token
            // var customerToken = await _customerService.CreateCustomerToken(customer);
            
            // Confirm Customer Email
            // var retrievedCustomer = await _customerService.ConfirmCustomerEmail(customerToken);

            // Console.WriteLine($"Is Email Verified:{retrievedCustomer.IsEmailVerified}");
        }
    }
}