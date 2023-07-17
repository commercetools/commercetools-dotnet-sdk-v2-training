using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
     /// <summary>
    /// ASSIGN the customer to the customer group
    /// </summary>
    public class Task02B : IExercise
    {
        private readonly IClient _client;
        private const string _customerKey = "nd-customer";
        private const string _customerGroupKey = "vip-customers";

        private readonly CustomerService _customerService;

        public Task02B(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customerService = new CustomerService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {

            // SET customerGroup for the customer
            var customer = await _customerService.AssignCustomerToCustomerGroup(
                    _customerKey,
                    _customerGroupKey
                );

            Console.WriteLine($"customer {customer.Id} in customer group {customer.CustomerGroup.Id}");
        }
    }
}