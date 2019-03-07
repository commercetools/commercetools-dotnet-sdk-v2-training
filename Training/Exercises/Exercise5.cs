using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Products;

namespace Training
{
    /// <summary>
    /// Add Product to Category Exercise
    /// </summary>
    public class Exercise5 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise5(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            AddProductToCategory();
        }
        
        private void AddProductToCategory()
        {
            //retrieve category by key
            Category category =
                _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Category>(Settings.CATEGORYKEY)).Result;
            
            //retrieve product by key
            Product product =
                _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Product>(Settings.PRODUCTTYPEKEY)).Result;
            
            
            //In the second Day
            
            
            
            //Create AddToCategoryUpdateAction
            AddToCategoryUpdateAction addToCategoryUpdateAction = new AddToCategoryUpdateAction()
            {
                OrderHint = Settings.RandomSortOrder(),
                Category =  new ResourceIdentifier() { Key = Settings.CATEGORYKEY}
            };
            
            List<UpdateAction<Product>> updateActions = new List<UpdateAction<Product>>();
            updateActions.Add(addToCategoryUpdateAction);
            
            //Add the category to the product
            Product retrievedProduct = _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Product>(Settings.PRODUCTTYPEKEY, product.Version, updateActions))
                .Result;
            
            //show product categories
            foreach (var cat in retrievedProduct.MasterData.Current.Categories)
            {
                Console.WriteLine($"Category ID {cat.Id}");
            }
        }
    }
}