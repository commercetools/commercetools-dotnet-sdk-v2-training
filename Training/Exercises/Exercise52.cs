using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Categories;

namespace Training
{
    /// <summary>
    /// PageQueryRequests Optimized, Query all Categories in optimized way
    /// https://docs.commercetools.com/http-api#paging
    /// </summary>
    public class Exercise52 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise52(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            string lastId = null ;int pageSize = 20;int currentPage = 1; bool lastPage = false;

            var queryCommand = new QueryCommand<Category>();
            queryCommand.Sort(category => category.Id);//sort By Id asc
            queryCommand.SetLimit(pageSize); //
            queryCommand.SetWithTotal(false);
            while (!lastPage)
            {
                if (lastId != null)
                {
                    //queryCommand.Where(category => category.Id > lastId);
                    queryCommand.SetWhere($"id > \"{lastId}\"");
                }
                var returnedSet = await _commercetoolsClient.ExecuteAsync(queryCommand);
                Console.WriteLine($"Show Results of Page {currentPage}");
                foreach (var category in returnedSet.Results)
                {
                    Console.WriteLine($"{category.Name["en"]}");
                }
                Console.WriteLine("///////////////////////");
                currentPage++;
                lastId = returnedSet.Results.Last().Id;
                lastPage = returnedSet.Results.Count < pageSize;
            }
        }


    }
}
