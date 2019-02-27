using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Products;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Add Product to Category Exercise
    /// </summary>
    public class Exercise5 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        private static Random random = new Random();
        
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
            string productKey = "Product1-Key-123";
            string categoryKey = "Category1-Key-123";
            
            //retrieve product by key
            Product product =
                _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Product>(productKey)).Result;
            
            AddToCategoryUpdateAction addToCategoryUpdateAction = new AddToCategoryUpdateAction()
            {
                OrderHint = this.RandomSortOrder(),
                Category =  new ResourceIdentifier() { Key = categoryKey}
            };
            
            List<UpdateAction<Product>> updateActions = new List<UpdateAction<Product>>();
            updateActions.Add(addToCategoryUpdateAction);
            
            Product retrievedProduct = _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Product>(productKey, product.Version, updateActions))
                .Result;
            
            //show product categories
            foreach (var category in retrievedProduct.MasterData.Current.Categories)
            {
                Console.WriteLine($"Category ID {category.Id}");
            }
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