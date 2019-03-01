using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Products;
using commercetools.Sdk.Domain.Query;
using System.Threading.Tasks;

namespace Training
{
    /// <summary>
    /// Add Product to Category Exercise - Good Solution
    /// </summary>
    public class Exercise5B : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        private static Random random = new Random();
        
        public Exercise5B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            AddProductToCategoryAsync();
        }
        
        /// <summary>
        /// Good Solution
        /// </summary>
        private async void AddProductToCategoryAsync()
        {
            
            string productKey = "Product1-Key-123";
            string categoryKey = "Category1-Key-123";

            var retrieveCategoryTask = _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Category>(categoryKey));
            var retrieveProductTask = _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Product>(productKey));


            var updatedProduct = await Task.WhenAll(retrieveCategoryTask, retrieveProductTask).ContinueWith(
                retrieveTask => AddCategoryToProductTask(retrieveCategoryTask.Result, retrieveProductTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();
            
            //show product categories
            foreach (var cat in updatedProduct.MasterData.Current.Categories)
            {
                Console.WriteLine($"Category ID {cat.Id}");
            }

        }

        /// <summary>
        /// Create AddCategoryToProduct Task
        /// </summary>
        /// <param name="category"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private Task<Product> AddCategoryToProductTask(Category category, Product product)
        {
            AddToCategoryUpdateAction addToCategoryUpdateAction = new AddToCategoryUpdateAction()
            {
                OrderHint = this.RandomSortOrder(),
                Category = new ResourceIdentifier() {Key = category.Key}
            };

            List<UpdateAction<Product>> updateActions = new List<UpdateAction<Product>>();

            updateActions.Add(addToCategoryUpdateAction);
            return _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Product>(product.Key, product.Version, updateActions));            
        }
        
        /// <summary>
        /// String representing a number which is greater than 0 and smaller than 1. It should start with “0.” and should not end with “0”.
        /// </summary>
        /// <returns></returns>
        public string RandomSortOrder()
        {
            int append = 5;//hack to not have a trailing 0 which is not accepted in sphere
            return "0." + random.Next() + append;
        }
    }
}