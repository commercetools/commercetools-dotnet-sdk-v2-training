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
            

            // GET All Products
            
            
            //  GET all products for category plants

            

            //  GET all products for category plants, facets on size and weight

            
            
            //variants.attributes.weight_in_kg:range (0 to *) as weight
            
            
            //  GET all products for category plants, facets on size and weight, filter on products on price between 1 Euro and 100 Euro WITHOUT effects on facets
            
            ////  GET all products for category plants, facets on size and weight, filter on products on price between 1 Euro and 100 Euro WITHOUT effects on facets
            //           click on a facet
            
            //filter.facets=variants.attributes.size.label:"package"
            
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