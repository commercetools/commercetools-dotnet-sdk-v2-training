using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.CustomerGroups;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Customers.UpdateActions;
using commercetools.Sdk.HttpApi.CommandBuilders;

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
        private readonly string _customerkey = "customer-michael-888663958";
        private readonly string _customerGroupKey = "diamond";

        public Task02B(IClient commercetoolsClient)
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
            //  Get customer by Key
            var customer = await _client.ExecuteAsync(new
                GetByKeyCommand<Customer>(_customerkey));

            //  Get customer group by Key
            var customerGroup = await _client.ExecuteAsync(new
                GetByKeyCommand<CustomerGroup>(_customerGroupKey));

            // set customerGroup for the customer
            var action = new SetCustomerGroupUpdateAction
            {
                CustomerGroup = customerGroup.ToKeyResourceIdentifier()
            };
            var updatedCustomer = await _client
                .ExecuteAsync(customer.UpdateByKey(
                    actions => actions.AddUpdate(action)).Expand(c => c.CustomerGroup));

            Console.WriteLine($"customer {updatedCustomer.Id} in customer group " +
                              $"{updatedCustomer.CustomerGroup.Obj.Name}");
        }

        private async Task ExecuteByBuilder()
        {
            //  Get customer by Key
            var customer = await _client.Builder().Customers().GetByKey(_customerkey).ExecuteAsync();

            //  Get customer group by Key
            var customerGroup = await _client.Builder().CustomerGroups().GetByKey(_customerGroupKey).ExecuteAsync();

            // set customerGroup for the customer
            var action = new SetCustomerGroupUpdateAction
            {
                CustomerGroup = customerGroup.ToKeyResourceIdentifier()
            };
            var updatedCustomer =
                await _client
                    .Builder()
                    .Customers()
                    .UpdateByKey(customer)
                    .AddAction(action)
                    .ExecuteAsync();

            Console.WriteLine($"customer {updatedCustomer.Id} in customer group " +
                              $"{updatedCustomer.CustomerGroup.Id}");
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