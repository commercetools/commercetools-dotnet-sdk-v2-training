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
            

            //  Get customer group by Key
            

            // set customerGroup for the customer
            
            //Console.WriteLine($"customer {updatedCustomer.Id} in customer group {updatedCustomer.CustomerGroup.Id}");
        }
    }
}