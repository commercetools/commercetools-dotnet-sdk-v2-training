using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Products;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Examples of how to use Search ProductProjections
    /// params found in the product projection search https://docs.commercetools.com/api/projects/products-search#search-productprojections
    /// </summary>
    public class Task06A : IExercise
    {
        private readonly IClient _client;
        private readonly SearchService _searchService;

        private const string _productTypeKey = "PhonePT";
        

        public Task06A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _searchService = new SearchService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            // GET productType
            var productType = await _searchService.GetProductTypeByKey(_productTypeKey);

            var filterQuery = $"productType.id:\"{productType.Id}\"";
            var facet = "variants.attributes.phonecolor as color";
            var response = await _searchService.GetSearchResults(filterQuery, facet);
            
            Console.WriteLine($"No. of products: {response.Count}");
            Console.WriteLine("products in search result: ");
            response.Results.ForEach(p => Console.WriteLine(p.Name["de"]));
            
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