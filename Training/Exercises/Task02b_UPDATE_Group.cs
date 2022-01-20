using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
     /// <summary>
    /// GET a customer
    /// GET a customer group
    /// ASSIGN the customer to the customer group
    /// </summary>
    public class Task02B : IExercise
    {
        private readonly IClient _client;
        private readonly string _customerKey = "customer-michele-george";
        private readonly string _customerGroupKey = "indoor-customer-group";

        private readonly CustomerService _customerService;

        public Task02B(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            
            // set customerGroup for the customer
            var updatedCustomer = await _customerService.AssignCustomerToCustomerGroup(_customerKey,_customerGroupKey);

            Console.WriteLine($"customer {updatedCustomer.Id} in customer group {updatedCustomer.CustomerGroup.Id}");
        }
    }
}