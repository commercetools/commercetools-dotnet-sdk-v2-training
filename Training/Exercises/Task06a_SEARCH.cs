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
            // GET the productType by Key
            

            //Search ProductProjections and Add Facet
            ProductProjectionPagedSearchResponse response = null;
            
            //Show Search Results
            Console.WriteLine($"Nr. of products: {response.Count}");
            Console.WriteLine("products in search result: ");
            response.Results.ForEach(p => Console.WriteLine(p.Name["en"]));
            
            //Show Facets
            ShowFacetResults(response);
        }

        private void ShowFacetResults(ProductProjectionPagedSearchResponse searchResponse)
        {
            Console.WriteLine($"Number of Facets: {searchResponse.Facets.Count}");

            var colorFacetResult = searchResponse.Facets["color"] as TermFacetResult;
            foreach (var term in colorFacetResult.Terms)
            {
                Console.WriteLine($"Term : {term.Term}, Count: {term.Count}");
            }
        }
    }
}