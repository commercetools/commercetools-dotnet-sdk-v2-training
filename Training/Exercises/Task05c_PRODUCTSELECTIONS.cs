using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Common;
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
        private const string _storekey = "berlin-store";
        private const string _productSelectionKey = "berlin-product-selection";
        private readonly ProductSelectionService _productSelectionService;
        private readonly StoreService _storeService;


        public Task05C(IClient client)
        {
            this._client = client;
            this._productSelectionService = new ProductSelectionService(_client,Settings.ProjectKey);
            this._storeService = new StoreService(_client,Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {

            var productSelection = await _productSelectionService.CreateProductSelection(_productSelectionKey,new LocalizedString {{"en","Berlin Store Selection"},{"de","Berlin Store Selection"}});
            
            System.Console.WriteLine($"Product selection created: {productSelection.Id}"); 
            
            var berlinProductSelection = await _productSelectionService.GetProductSelectionByKey(_productSelectionKey);

            System.Console.WriteLine($"Berlin Product selection: {berlinProductSelection.Id} with {berlinProductSelection.ProductCount} products");

            var updatedProductSelection = await _productSelectionService.AddProductToProductSelection(_productSelectionKey,"tulip-seed-product");

            System.Console.WriteLine($"Berlin Product selection: {updatedProductSelection.Id} with {updatedProductSelection.ProductCount} products");

            var berlinStore = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Stores()
                .WithKey(_storekey)
                .Get()
                .ExecuteAsync();
            
            var updatedStore = await _storeService.AddProductSelectionsToStore(_productSelectionKey,berlinStore);
                
            System.Console.WriteLine($"Updated store {berlinStore.Key} with selection {updatedStore.ProductSelections?.Count}");

            var productSelectionProducts = await _productSelectionService.GetProductSelectionProductByKey(_productSelectionKey);
            System.Console.WriteLine($"Products in the product selection: {productSelectionProducts.Results.Count}");
            foreach (var product in productSelectionProducts.Results)
            {
                System.Console.WriteLine(product.Product.Obj.Key);
            }

            var productsInStore = await _storeService.GetProductsInStore(_storekey);
            System.Console.WriteLine($"Products in the store through product selections: {productsInStore.Results.Count}");
            foreach (var product in productsInStore.Results)
            {
                System.Console.WriteLine($"{product.Product.Id} through {product.ProductSelection.Id}");
            } 

        }
    }
}