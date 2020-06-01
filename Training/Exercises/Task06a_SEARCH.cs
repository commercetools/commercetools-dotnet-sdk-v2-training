using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.ProductProjections;
using commercetools.Sdk.Domain.Products.Attributes;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// Examples of how to use SearchProductProjectionsCommand
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
            // GET plantSeedCategory
            var plantSeedCategory = await _client.Builder()
                .Categories().GetByKey("plant-seeds").ExecuteAsync();

            // GET All Products
            var search1Command = new SearchProductProjectionsCommand();
            search1Command.SetStaged(false); //current

            var searchResults = await _client.ExecuteAsync(search1Command);
            Console.WriteLine($"Count of Current Products: {searchResults.Count}");

            //  GET all products for category plants

            var search2Command = new SearchProductProjectionsCommand();
            search2Command.SetStaged(false); //current
            search2Command.MarkMatchingVariants(true);
            search2Command.FilterQuery(p => p.Categories.Any(
                c => c.Id == plantSeedCategory.Id.valueOf()));

            searchResults = await _client.ExecuteAsync(search2Command);
            Console.WriteLine($"Count of Current Products in category plants: {searchResults.Count}");

            //  GET all products for category plants, facets on size and weight

            var search3Command = new SearchProductProjectionsCommand();
            search3Command.SetStaged(false); //current
            search3Command.MarkMatchingVariants(true);
            search3Command.FilterQuery(p => p.Categories.Any(
                c => c.Id == plantSeedCategory.Id.valueOf()));
            //variants.attributes.size.label as size
            search3Command.TermFacet("size",
                p => p.Variants.Select(v => v.Attributes.Where(a => a.Name == "size")
                    .Select(a => ((EnumAttribute) a).Value.Label).FirstOrDefault()).FirstOrDefault());
            
            //variants.attributes.weight_in_kg:range (0 to *) as weight
            search3Command.RangeFacet("weight", p => p.Variants.Any(
                v => v.Attributes.Any(a => a.Name == "weight_in_kg" &&
                                           ((NumberAttribute) a).Value.Range(0, null)
                )),true);

            searchResults = await _client.ExecuteAsync(search3Command);
            Console.WriteLine("GET all products for category plants, facets on size and weight");
            ShowFacetResults(searchResults);
            
            //  GET all products for category plants, facets on size and weight, filter on products on price between 1 Euro and 100 Euro WITHOUT effects on facets
            search3Command.Filter(p => p.Variants.Any(v => v.Price.Value.CentAmount.Range(100, 10000)));
            searchResults = await _client.ExecuteAsync(search3Command);
            Console.WriteLine("GET all products for category plants, facets on size and weight, filter on products on price between 1 Euro and 100 Euro WITHOUT effects on facets");
            ShowFacetResults(searchResults);
            
            ////  GET all products for category plants, facets on size and weight, filter on products on price between 1 Euro and 100 Euro WITHOUT effects on facets
            //           click on a facet
            
            //filter.facets=variants.attributes.size.label:"package"
            search3Command.FilterFacets(
                p => p.Variants.Any(v => v.Attributes.Any(a => a.Name == "size" && ((EnumAttribute) a).Value.Label == "package")));
            searchResults = await _client.ExecuteAsync(search3Command);
            Console.WriteLine("click on package facet");
            ShowFacetResults(searchResults);
        }

        private void ShowFacetResults(PagedQueryResult<ProductProjection> searchResults)
        {
            var sizeFacetResult = searchResults.Facets["size"] as TermFacetResult;
            Console.WriteLine($"Count of Current Products in category plants: {searchResults.Count}");
            foreach (var term in sizeFacetResult.Terms)
            {
                Console.WriteLine($"Term : {term.Term}, Count: {term.Count}");
            }

            var weightFacetResult = searchResults.Facets["weight"] as RangeFacetResult;
            Console.WriteLine($"number of ranges: {weightFacetResult?.Ranges.Count}");
            var range = weightFacetResult?.Ranges[0];
            Console.WriteLine($"Min: {range.Min}, " +
                              $"Max:{range.Max}, " +
                              $"Mean:{range.Mean}, " +
                              $"ProductsCount:{range.ProductCount}");
        }
    }
}