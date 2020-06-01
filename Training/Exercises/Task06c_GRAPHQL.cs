using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.GraphQL;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Task06C : IExercise
    {
        private readonly IClient _client;
        
        public Task06C(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            string graphQuery = "query {customers{count,results{email}}}";
            
            throw new NotImplementedException();
        }
    }
}