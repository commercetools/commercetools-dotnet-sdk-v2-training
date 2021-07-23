using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.CustomerGroups;
using commercetools.Api.Models.Customers;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

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
        private readonly string _customerkey = "ronnieWood";
        private readonly string _customerGroupKey = "gold";

        public Task02B(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            //  Get customer by Key
            var customer = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Customers()
                .WithKey(_customerkey)
                .Get()
                .ExecuteAsync();

            //  Get customer group by Key
            var customerGroup = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .CustomerGroups()
                .WithKey(_customerGroupKey)
                .Get()
                .ExecuteAsync();

            // set customerGroup for the customer
            var update = new CustomerUpdate
            {
                Version = customer.Version,
                Actions = new List<ICustomerUpdateAction>
                {
                    new CustomerSetCustomerGroupAction
                    {
                        CustomerGroup = new CustomerGroupResourceIdentifier
                        {
                            Key = customerGroup.Key
                        }
                    }
                }
            };
            var updatedCustomer =
                await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Customers()
                    .WithKey(customer.Key)
                    .Post(update)
                    .ExecuteAsync();

            Console.WriteLine($"customer {updatedCustomer.Id} in customer group " +
                              $"{updatedCustomer.CustomerGroup.Id}");
        }
    }
}