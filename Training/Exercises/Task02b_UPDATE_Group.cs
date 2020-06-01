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
           

            //  Get customer group by Key
           

            // set customerGroup for the customer (using SetCustomerGroupUpdateAction)
           
        }

        private async Task ExecuteByBuilder()
        {
            //  Get customer by Key
            

            //  Get customer group by Key
            

            // set customerGroup for the customer
        }
    }
}