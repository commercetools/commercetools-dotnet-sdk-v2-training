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

            var queryCommand = new QueryCommand<Product>();
            queryCommand.Sort(product => product.Id);//sort By Id asc
            queryCommand.SetLimit(pageSize); //
            queryCommand.SetWithTotal(false);
            while (!lastPage)
            {
                if (lastId != null)
                {
                    queryCommand.Where(p => p.Id.IsGreaterThan(lastId));
                    //queryCommand.SetWhere($"id > \"{lastId}\"");
                }
                var returnedSet = await _client.ExecuteAsync(queryCommand);
                Console.WriteLine($"Show Results of Page {currentPage}");
                foreach (var product in returnedSet.Results)
                {
                    Console.WriteLine($"{product.MasterData.Current.Name["en"]}");
                }
                Console.WriteLine("///////////////////////");
                currentPage++;
                lastId = returnedSet.Results.Last().Id;
                lastPage = returnedSet.Results.Count < pageSize;
            }
        }
    }
}