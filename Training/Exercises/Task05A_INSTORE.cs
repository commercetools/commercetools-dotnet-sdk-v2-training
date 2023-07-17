using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Services;

namespace Training
{
    //Get Customers in specific Store
    public class Task05A : IExercise
    {

        private readonly IClient _storeClient;
        private readonly StoreService _storeService;
        private readonly CustomerService _customerService;
        private const string _storeKey = "berlin-store";
        private const string _customerKey = "nd-customer";

        public Task05A(IEnumerable<IClient> clients)
        {
            this._storeClient = clients.FirstOrDefault(c => c.Name.Equals("StoreClient"));
            _storeService = new StoreService(_storeClient,Settings.ProjectKey);
            _customerService = new CustomerService(_storeClient,Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            //var customers = await _storeService.GetCustomersInStore(_storeKey);
            //Console.WriteLine($"Store customers (including global): {customers.Total}");

            var storeCart = await _storeService.CreateInStoreCart(_customerKey, _storeKey);
            System.Console.WriteLine($"Cart {storeCart.Id} created in store {_storeKey} for customer {_customerKey}");
        }
    }
}