using System;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Products;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Examples of how to use Search ProductProjections
    /// params found in the product projection search https://docs.commercetools.com/api/projects/products-search#search-productprojections
    /// </summary>
    public class Task06A : IExercise
    {
        private readonly IClient _client;

        public Task06A(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            // GET plantSeedCategory
            var category = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Categories().WithKey("c3").Get().ExecuteAsync();

            var response = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .ProductProjections()
                .Search()
                .Get()
                .WithStaged(false)
                .WithMarkMatchingVariants(true)
                .WithFilterQuery($"categories.id:\"{category.Id}\"")
                .WithFacet("variants.attributes.size as size")
                //.WithFacet("variants.price.centAmount:range (* to 20000), (20000 to 30000), (30000 to *) as ranges")
                //.AddQueryParam("text.de", localizedName["de"])
                .ExecuteAsync();

            var results = response.Results;
            Console.WriteLine($"Nr. of products: {results.Count}");

            ShowFacetResults(response);

            Console.WriteLine("products searched: ");
            results.ForEach(p => Console.WriteLine(p.Name["en"]));
        }

        private void ShowFacetResults(ProductProjectionPagedSearchResponse searchResponse)
        {
            Console.WriteLine($"Facets: {searchResponse.Facets.Count}");

            var sizeFacetResult = searchResponse.Facets["size"] as TermFacetResult;
            Console.WriteLine($"Count of Current Products in category plants: {searchResponse.Count}");
            foreach (var term in sizeFacetResult.Terms)
            {
                Console.WriteLine($"Term : {term.Term}, Count: {term.Count}");
            }

            /*
            var rangeFacetResult = searchResponse.Facets["ranges"] as RangeFacetResult;
            Console.WriteLine($"number of ranges: {rangeFacetResult?.Ranges.Count}");
            var range = rangeFacetResult?.Ranges[0];
            Console.WriteLine($"Min: {range.Min}, " +
                              $"Max:{range.Max}, " +
                              $"Mean:{range.Mean}, " +
                              $"ProductsCount:{range.ProductCount}");
            */
        }
    }
}