using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.GraphQL;
using Training.GraphQL;
using Training.MachineLearningExtensions;

namespace Training
{
    /// <summary>
    /// MachineLearning Exercise
    /// </summary>
    public class Task08B : IExercise
    {
        private readonly IClient _client;
        
        public Task08B(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._client = clients.FirstOrDefault(c => c.Name == "MachineLearningClient");// the machine learning client
        }

        public async Task ExecuteAsync()
        {
            // Get categories recommendations using product name
            
            // Get categories recommendations using product image url

            throw new NotImplementedException();
        }
    }
}