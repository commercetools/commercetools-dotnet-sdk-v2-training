using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// CREATE a customer
    /// CREATE a email verfification token
    /// Verfify customer
    /// </summary>
    public class Task02A : IExercise
    {
        private readonly IClient _client;

        public Task02A(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }

            this._client = clients.FirstOrDefault(c => c.Name == "Client"); // the default client
        }

        public async Task ExecuteAsync()
        {
            await ExecuteByCommands();
            //await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            //  SignUp a customer
            
            
            //  CREATE a email verfification token
            
        }

        private async Task ExecuteByBuilder()
        {
            //  SignUp a customer
            
            
            
            // Verify the customer
          
        }

        /// <summary>
        /// Get Customer Draft
        /// </summary>
        /// <returns></returns>
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