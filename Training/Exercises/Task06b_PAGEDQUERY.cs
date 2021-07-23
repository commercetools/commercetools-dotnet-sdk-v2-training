using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// PageQueryRequests Optimized, Query all Products in optimized way
    /// https://docs.commercetools.com/http-api#paging
    /// </summary>
    public class Task06B : IExercise
    {
        private readonly IClient _client;
        
        public Task06B(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            string lastId = null ;int pageSize = 2;int currentPage = 1; bool lastPage = false;

            while (!lastPage)
            {
                var where = lastId != null ? $"id>\"{lastId}\"" : null;
                
                var response = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Products()
                    .Get()
                    .WithSort("id asc")
                    .WithLimit(pageSize)
                    .WithWhere(where)
                    .WithWithTotal(false)
                    .ExecuteAsync();

                Console.WriteLine($"Show Results of Page {currentPage}");
                foreach (var product in response.Results)
                {
                    Console.WriteLine($"{product.MasterData.Current.Name["en"]}");
                }
                Console.WriteLine("///////////////////////");
                currentPage++;
                lastId = response.Results.Last().Id;
                lastPage = response.Results.Count < pageSize;
            }
        }
    }
}