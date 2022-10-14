using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using commercetools.Sdk.Api.Models.Products;

namespace Training
{
    /// <summary>
    /// Examples of how to use Search ProductProjections
    /// params found in the product projection search https://docs.commercetools.com/api/projects/products-search#search-productprojections
    /// </summary>
    public class Task06A : IExercise
    {
        private readonly IClient _client;

        private const string _productTypeKey = "PhonePT";


        public Task06A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
        }

        public async Task ExecuteAsync()
        {
            // GET productType
            var productType = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductTypes()
                .WithKey(_productTypeKey)
                .Get()
                .ExecuteAsync();

            var filterQuery = $"productType.id:\"{productType.Id}\"";
            var facet = "variants.attributes.phonecolor as color";

            // TODO: GET product projections paged search response with facets
            IProductProjectionPagedSearchResponse response = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .ProductProjections()
                .Search()
                .Get()
                .WithStaged(true)
                .WithFilterQuery(filterQuery)
                .WithFacet(facet)
                .WithWithTotal(false)
                .ExecuteAsync();
            
            //Show Search Results
            Console.WriteLine($"No. of products: {response.Count}");
            Console.WriteLine("products in search result: ");
            response.Results.ForEach(p => Console.WriteLine(p.Name["en"]));
            
            //Show Facets
            ShowFacetResults(response);
        }

        private void ShowFacetResults(IProductProjectionPagedSearchResponse searchResponse)
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