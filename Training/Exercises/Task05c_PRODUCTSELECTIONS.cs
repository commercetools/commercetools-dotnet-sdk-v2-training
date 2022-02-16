using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Create a Product Selection
    /// GET a Product Selection
    /// ADD/REMOVE products to/from the Product Selections
    /// ADD/REMOVE Product Selections to/from the stores
    /// </summary>
    public class Task05C : IExercise
    {
        private readonly IClient _client;
        private const string _storekey = "";
        private const string _productSelectionKey = "";
        private readonly ProductSelectionService _productSelectionService;


        public Task05C(IClient client)
        {
            this._client = client;
            this._productSelectionService = new ProductSelectionService(_client,Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {

            // TODO: CREATE a product selection
            
            // System.Console.WriteLine($"Product selection: {productSelection.Id} with {productSelection.ProductCount} products");

            // TODO: ADD a product to the product selection

            // System.Console.WriteLine($"Berlin Product selection: {updatedProductSelection.Id} with {updatedProductSelection.ProductCount} products");

            // TODO: GET store by key
            var store = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Stores()
                .WithKey(_storekey)
                .Get()
                .ExecuteAsync();
            
            // TODO: set product selection for the store
                
            // System.Console.WriteLine($"Updated store {store.Key} with selection {updatedStore.ProductSelections?.Count}");
        }
    }
}