using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Predicates;

namespace Training
{
    /// <summary>
    /// PageQueryRequests Optimized, Query all Products in optimized way
    /// https://docs.commercetools.com/http-api#paging
    /// </summary>
    public class Task06B : IExercise
    {
        private readonly IClient _client;
        

        public Task06B(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            string lastId = null ;int pageSize = 2;int currentPage = 1; bool lastPage = false;

            throw new NotImplementedException();
        }
    }
}