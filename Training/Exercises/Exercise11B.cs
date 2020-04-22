using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Products.UpdateActions;

namespace Training
{
    /// <summary>
    /// Add Product to Category Exercise - Good Solution
    /// </summary>
    public class Exercise11B : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise11B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            var retrieveCategoryTask =
                _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Category>(Settings.CATEGORYKEY));
            var retrieveProductTask =
                _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Product>(Settings.PRODUCTKEY));


            var updatedProduct = await Task.WhenAll(retrieveCategoryTask, retrieveProductTask).ContinueWith(
                retrieveTask => AddCategoryToProductTask(retrieveCategoryTask.Result, retrieveProductTask.Result),
                TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();

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
                OrderHint = Settings.RandomSortOrder(),
                Category = new ResourceIdentifier<Category>
                {
                    Key = category.Key
                }
            };

            List<UpdateAction<Product>> updateActions = new List<UpdateAction<Product>>
            {
                addToCategoryUpdateAction
            };

            return _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Product>(product.Key, product.Version, updateActions));
        }
    }
}
