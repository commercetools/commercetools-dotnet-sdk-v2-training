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
    /// Add Product to Category Exercise
    /// </summary>
    public class Exercise11A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise11A(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //retrieve category by key
            Category category =
                await _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Category>(Settings.CATEGORYKEY));

            //retrieve product by key
            Product product =
                await _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Product>(Settings.PRODUCTKEY));


            //In the second Day

            //Create AddToCategoryUpdateAction
            AddToCategoryUpdateAction addToCategoryUpdateAction = new AddToCategoryUpdateAction()
            {
                OrderHint = Settings.RandomSortOrder(),
                Category = new ResourceIdentifier<Category>
                {
                    Key = Settings.CATEGORYKEY
                }
            };

            List<UpdateAction<Product>> updateActions = new List<UpdateAction<Product>>
            {
                addToCategoryUpdateAction
            };

            //Add the category to the product
            Product retrievedProduct = await _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Product>(Settings.PRODUCTKEY, product.Version, updateActions));

            //show product categories
            foreach (var cat in retrievedProduct.MasterData.Current.Categories)
            {
                Console.WriteLine($"Category ID {cat.Id}");
            }
        }
    }
}
