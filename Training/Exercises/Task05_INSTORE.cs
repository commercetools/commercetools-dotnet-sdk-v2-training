using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    //Get Customers in specific Store
    public class Task05 : IExercise
    {
        private readonly IClient _berlinStoreClient;

        public Task05(IEnumerable<IClient> clients)
        {
            this._berlinStoreClient = clients.FirstOrDefault(c => c.Name.Equals("BerlinStoreClient"));
        }

        public async Task ExecuteAsync()
        {
            var customers = await _berlinStoreClient.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .InStoreKeyWithStoreKeyValue("berlin-store")
                .Customers().Get().ExecuteAsync();
            Console.WriteLine($"Global customers and customers in Berlin Store: {customers.Count}");
        }
    }
}