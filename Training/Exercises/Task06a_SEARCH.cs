using System;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Products;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Examples of how to use SearchProductProjectionsCommand
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
            var plantSeedCategory = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Categories().WithKey("c3").Get().ExecuteAsync();

            // the effective filter from the search response
            // params found in the product projection search https://docs.commercetools.com/api/projects/products-search#search-productprojections
            var response = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .ProductProjections()
                .Search()
                .Get()
                .WithStaged(false)
                .WithMarkMatchingVariants(true)//Restrict on category plant-seeds
                .WithFilterQuery($"categories.id:\"{plantSeedCategory.Id}\"")
                // TODO Get all Facets for Enum size and Number weight_in_kg
                .WithFacet("variants.attributes.size as size")

                // TODO Give price range on products with no effect on facets
                // .withFilter("variants.price.centAmount:range (100 to 100000)")
                // TODO: with effect on facets
                // .withFilterQuery("variants.price.centAmount:range (100 to 100000)")

                // TODO: Simulate click on facet box from attribute size
                // .withFilterFacets("variants.attributes.size.label:\"box\"")
                .ExecuteAsync();

            var results = response.Results;
            Console.WriteLine($"Nr. of products: {results.Count}");
            
            ShowFacetResults(response);

            Console.WriteLine("products searched: ");
            results.ForEach(p => Console.WriteLine(p.Key));
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

            var weightFacetResult = searchResponse.Facets["filtersize"] as RangeFacetResult;
            Console.WriteLine($"number of ranges: {weightFacetResult?.Ranges.Count}");
            var range = weightFacetResult?.Ranges[0];
            Console.WriteLine($"Min: {range.Min}, " +
                              $"Max:{range.Max}, " +
                              $"Mean:{range.Mean}, " +
                              $"ProductsCount:{range.ProductCount}");
        }
    }
}