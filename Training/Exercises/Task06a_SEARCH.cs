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
            // GET productType
            var productType = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .ProductTypes().WithKey("PhonePT").Get().ExecuteAsync();

            var response = await _client.WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .ProductProjections()
                .Search()
                .Get()
                .WithStaged(false)
                .WithMarkMatchingVariants(true)
                .WithFilterQuery($"productType.id:\"{productType.Id}\"")
                .WithFacet("variants.attributes.phonecolor as color")
                //.AddQueryParam("text.en", "IPhome11")
                //.WithFuzzy(true)
                .ExecuteAsync();
            
            Console.WriteLine($"Nr. of products: {response.Count}");
            Console.WriteLine("products in search result: ");
            response.Results.ForEach(p => Console.WriteLine(p.Name["en"]));
            
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