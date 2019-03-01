using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Add new Product Exercise
    /// </summary>
    public class Exercise4B : IExercise
    {
        private static Random random = new Random(); 
        private readonly IClient _commercetoolsClient;
        
        public Exercise4B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            AddNewProduct();
        }

        private void AddNewProduct()
        {
            // get the product draft first
            var productDraft = GetProductDraft();
            // create the product
            Product product = _commercetoolsClient.ExecuteAsync(new CreateCommand<Product>(productDraft)).Result;
            
            //Display Product Name
            Console.WriteLine($"New Product Created with name: {product.MasterData.Staged.Name["en"]}");
        }
        
        public ProductDraft GetProductDraft()
        {
            string productTypeKey = "Jacket-PT";
            string categoryKey = "Category1-Key-123";
            
            //Get the Product Type
            ProductType productType = _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<ProductType>(productTypeKey)).Result;
            
            //Get the Category
            Category category = _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Category>(categoryKey)).Result;
            
            //Create the Product Draft
            ProductDraft productDraft = new ProductDraft();
            productDraft.Name = new LocalizedString() {{"en", this.RandomString(4)}};
            productDraft.Key = this.RandomString(3);
            productDraft.Slug = new LocalizedString() {{"en", this.RandomString(3)}};
            productDraft.ProductType = new ResourceIdentifier() {Id = productType.Id};
            ProductVariantDraft productMasterVariant =this.GetProductVariantDraft();
            productDraft.MasterVariant = productMasterVariant;
            productDraft.Categories = new List<ResourceIdentifier>
            {
                new ResourceIdentifier() {Id = category.Id}
            };

            return productDraft;
        }
        public ProductVariantDraft GetProductVariantDraft()
        {
            var price = GetPriceDraft();
            var productVariantDraft = new ProductVariantDraft()
            {
                Key = this.RandomString(5),
                Sku = this.RandomString(5),
                Prices = new List<PriceDraft>(){price}
            };
            return productVariantDraft;
        }
        
        /// <summary>
        /// Get Price Draft
        /// </summary>
        /// <returns></returns>
        private PriceDraft GetPriceDraft()
        {
            var money = new Money()
            {
                CentAmount = 5000, 
                CurrencyCode = "EUR"
            };
            var priceDraft = new PriceDraft()
            {
                Value = money,
                Country = "DE"
            };
            return priceDraft;
        }
        
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}