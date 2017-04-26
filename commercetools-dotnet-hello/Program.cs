using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using commercetools.Common;
using commercetools.Common.UpdateActions;
using commercetools.Products;
using commercetools.Products.UpdateActions;
using commercetools.ProductTypes;
using commercetools.Project;
using commercetools.TaxCategories;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace commercetools
{
    class Program
    {
		private Client _client;
		private Project.Project _project;

        private ProductType _productType;
        private ProductDraft _productDraft;
        private TaxCategory _taxCategory;
        private Product _product;

        static void Main(string[] args)
        {
            new Program().Run().Wait();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private async Task Run()
        {
            Configuration config = new Configuration(
                "https://auth.sphere.io/oauth/token",
                "https://api.sphere.io",
                "tutorial-10",
                "fDA5lRyEyU9MN5BGeBpPbo7t",
                "IjzF-qL43_foq-W7OH9x2T5UpTkGeKL2",
                ProjectScope.ManageProject);

            _client = new Client(config);
            _project = GetProject(_client);

            // query all products
            await QueryAllProducts(_client);

            // Create new product
            _productType = GetProductType(_client);
            _taxCategory = GetTaxCategory(_client);
			_productDraft = Helper.GetTestProductDraft(_project, _productType.Id, _taxCategory.Id);
            _product = CreateProductAsync(_productDraft);

            UdpateProductAsync(_client);

            DeleteProduct(_client, _product);

        }

        // GET PROJECT

        private Project.Project GetProject(Client client){
			Task<Response<Project.Project>> projectTask = _client.Project().GetProjectAsync();
			projectTask.Wait();
			return projectTask.Result.Result;
        }

        private async Task QueryAllProducts(Client client)
        {
            Response<ProductQueryResult> response = await client.Products().QueryProductsAsync();

            if (response.Success)
            {
                ProductQueryResult productQueryResult = response.Result;
				Console.WriteLine("Queried products: ");
				foreach (Product product in productQueryResult.Results)
                {
                    Console.WriteLine(product.Id);
                }
            }
            else
            {
                Console.WriteLine("{0}: {1}", response.StatusCode, response.ReasonPhrase);

                foreach (ErrorMessage errorMessage in response.Errors)
                {
                    Console.WriteLine("{0}: {1}", errorMessage.Code, errorMessage.Message);
                }
            }
        }

        //CREATE PRODUCT

		/// <summary>
		/// Creates a new Product.
		/// </summary>
		/// <param name="productDraft">ProductDraft</param>
		/// <returns>Product</returns>
		/// <see href="http://dev.commercetools.com/http-api-projects-products.html#create-product"/>
        public Product CreateProductAsync(ProductDraft productDraft)
		{
			if (productDraft.Name == null || productDraft.Name.IsEmpty())
			{
				throw new ArgumentException("name is required");
			}

			if (productDraft.ProductType == null)
			{
				throw new ArgumentException("productType is required");
			}

			if (productDraft.Slug == null || productDraft.Slug.IsEmpty())
			{
				throw new ArgumentException("slug is required");
			}

			string payload = JsonConvert.SerializeObject(productDraft, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Product product = _client.PostAsync<Product>("/products", payload).Result.Result;
            Console.WriteLine("A new product with id '{0}' is created.", product.Id);

			return product;
		}

        private ProductType GetProductType(Client client)
        {
            ProductTypeDraft productTypeDraft = Helper.GetTestProductTypeDraft();
            Task<Response<ProductType>> productTypeTask = client.ProductTypes().CreateProductTypeAsync(productTypeDraft);
            productTypeTask.Wait();
            _productType = productTypeTask.Result.Result;
			
            Console.WriteLine("Product Type '{0}' is created.", _productType.Name);
			return _productType;
        }

        private TaxCategory GetTaxCategory(Client client){
			TaxCategoryDraft taxCategoryDraft = Helper.GetTestTaxCategoryDraft(_project);
			Task<Response<TaxCategory>> taxCategoryTask = _client.TaxCategories().CreateTaxCategoryAsync(taxCategoryDraft);
			taxCategoryTask.Wait();

            _taxCategory = taxCategoryTask.Result.Result;
            Console.WriteLine("Tax Category '{0}' is created.", _taxCategory.Name);
			return _taxCategory;
        }

        //UPDATE PRODUCT

        private Product UdpateProductAsync(Client client){
			SetKeyAction setKeyAction = new SetKeyAction();
			setKeyAction.Key = Helper.GetRandomString(15);

			LocalizedString newSlug = new LocalizedString();
			foreach (string language in _project.Languages)
			{
				newSlug.SetValue(language, string.Concat("updated-product-", language, "-", Helper.GetRandomString(10)));
			}
			GenericAction changeSlugAction = new GenericAction("changeSlug");
			changeSlugAction.SetProperty("slug", newSlug);
			changeSlugAction.SetProperty("staged", true);

			List<UpdateAction> actions = new List<UpdateAction>();
			actions.Add(setKeyAction);
			actions.Add(changeSlugAction);

            Response<Product> response = client.Products().UpdateProductAsync(_product, actions).Result;
            _product = response.Result;
            Console.WriteLine("Product with id '{0}' has been updated.", _product.Id);
            return _product;

		}

        //DELETE PRODUCT
        private async void DeleteProduct(Client client, Product product){
            Response<Product> response = await _client.Products().DeleteProductAsync(product.Id, product.Version);
            if (response.Success){
                Console.WriteLine("Product with id '{0}' has been deleted successfully", product.Id);
            }
		}
         
    }
}