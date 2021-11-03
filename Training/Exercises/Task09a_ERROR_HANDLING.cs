using System;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Base.Client.Error;
using commercetools.Sdk.Api.Extensions;

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
                //get non existing customer by key
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e.Body);
                throw;
            }
        }
    }
}