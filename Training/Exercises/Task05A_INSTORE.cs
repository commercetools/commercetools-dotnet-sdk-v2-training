using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Extensions;
using commercetools.Base.Client;

namespace Training
{
    //Get Customers in specific Store
    public class Task05 : IExercise
    {
        private readonly IClient _berlinStoreClient;

        public Task05A(IEnumerable<IClient> clients)
        {
            this._berlinStoreClient = clients.FirstOrDefault(c => c.Name.Equals("BerlinStoreClient"));
        }

        public async Task ExecuteAsync()
        {
            // use Store Client to Get Customers in Store By Key
            
            //Console.WriteLine($"Global customers and customers in Berlin Store: {customers.Count}");
        }
    }
}