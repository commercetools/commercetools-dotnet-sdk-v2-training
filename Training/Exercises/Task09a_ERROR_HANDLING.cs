using System;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Base.Client;
using commercetools.Base.Client.Error;

namespace Training
{
    public class Task09A : IExercise
    {
        private readonly IClient _client;

        public Task09A(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            var customerKeyMayOrMayNotExist = "customer-michele-WRONG-KEY";
            try
            {
                var optionalCustomer = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Customers()
                    .WithKey(customerKeyMayOrMayNotExist)
                    .Get()
                    .ExecuteAsync();
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e.Body);
                throw;
            }
        }
    }
}