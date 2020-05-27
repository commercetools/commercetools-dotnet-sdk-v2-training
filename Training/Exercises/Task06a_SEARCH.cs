using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;

namespace Training
{
    /// <summary>
    /// </summary>
    public class Task06A : IExercise
    {
        private readonly IClient _client;
        

        public Task06A(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
           
        }
    }
}