using System;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Products;
using commercetools.Base.Client;

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
            // UseCases
            // Fetching ALL products
            // Fetching ALL products of a certain type
            // Fetching ALL orders
            // Pagination of some entities BUT only ordered via id

            // Pagination is down to max 10.000
            var pageSize = 2;

            // Instead of asking for next page, ask for elements being greater than this id

            // TODO in class:
            // Give last id, start with slightly modified first id OR: do not use id when fetching first page
            // Give product type id
            //
            var lastId = "84cc7775-0ad5-4cf1-93dd-a2ec745a3c40";
            var productTypeId = "058a3465-6b40-4168-b2ab-3770d3964f98";

            //  link to give to our customers https://docs.commercetools.com/api/predicates/query

            var productPagedQueryResponse =
                    await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                            .Products()
                            .Get()
                            .WithWhere($"productType(id = {productTypeId})")
                            // Important, internally we use id > $lastId, it will not work without this line
                            .WithSort("id asc")
                            // Limit the size per page
                            .WithLimit(pageSize)
                            // use this for following pages
                            .WithWhere($"id > :{lastId}")
                            // always use this
                            .WithWithTotal(false)
                            .ExecuteAsync();

            // Print results
            Console.WriteLine("Found product size: " + productPagedQueryResponse.Results.Count);
            productPagedQueryResponse.Results.ForEach(
                    product => Console.WriteLine($"Product: {product.Id}"));
        }
    }
}