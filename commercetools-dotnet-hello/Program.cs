using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using commercetools.Common;
using commercetools.Common.UpdateActions;
using commercetools.Products;
using commercetools.Products.UpdateActions;
using commercetools.Categories;
using commercetools.Messages;
using commercetools.ProductTypes;
using commercetools.Project;
using commercetools.TaxCategories;
using commercetools.Carts;
using commercetools.Carts.UpdateActions;
using commercetools.Orders;
using commercetools.Orders.UpdateActions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace commercetools
{
    class Program
    {
		private Client _client;
		private Project.Project _project; // contains the project settings

        private ProductType _productType;
        private TaxCategory _taxCategory;
        private Product _product;
        private Category _category;
        private Cart _cart;
        private Order _order;

        static void Main()
        {
            new Program().Run().Wait();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private async Task Run()
        {
            //// PROJECT SIDE ////
            //TODO 1.1. Configure the client.
            Configuration config = new Configuration(
                "https://auth.sphere.io/oauth/token",
                "https://api.sphere.io",
                "training-test", // projectKey
                "", // clientID
                "", // clientSecret
                ProjectScope.ManageProject);

            _client = new Client(config);
            _project = GetProject(_client);

            _productType = CreateProductType(_client);
            _taxCategory = CreateTaxCategory(_client);
            _product = CreateProductAsync(_client);
            _category = CreateCategory(_client);
            _product = AddProductToCategory(_client);

            UdpateProductAsync(_client);

            // SHOP SIDE ////
            await QueryAllProducts(_client);
            _cart = CreateCart(_client);
            _cart = AddLineItemToCart(_product, _cart);
            _cart = RemoveLineItemfromCart(_cart, 0); // remove first lineitem
            _cart = AddLineItemToCart(_product, _cart);
            _order = CreateOrderFromCart(_cart);
            FinalizeOrder(_order);
            DeleteCart(_cart);
            DeleteProduct(_client, _product);

            //GetMessages(_client);
        }

        // GET PROJECT
        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <returns>The project.</returns>
        /// <param name="client">Client.</param>
        private Project.Project GetProject(Client client){
			Task<Response<Project.Project>> projectTask = client.Project().GetProjectAsync();
			projectTask.Wait();
			return projectTask.Result.Result;
        }

        //TODO 2.1. Create a product type
        /// <summary>
        /// Creates the type of the product.
        /// </summary>
        /// <returns>The product type.</returns>
        /// <param name="client">Client.</param>
        private ProductType CreateProductType(Client client)
        {
            ProductTypeDraft productTypeDraft = Helper.GetTestProductTypeDraft();
            Task<Response<ProductType>> productTypeTask = client.ProductTypes().CreateProductTypeAsync(productTypeDraft);
            productTypeTask.Wait();
            _productType = productTypeTask.Result.Result;

            Console.WriteLine("Product Type '{0}' is created.", _productType.Name);
            return _productType;
        }

        //TODO 2.2. Create a Tax Category
        /// <summary>
        /// Creates the tax category.
        /// </summary>
        /// <returns>The tax category.</returns>
        /// <param name="client">Client.</param>
        private TaxCategory CreateTaxCategory(Client client)
        {
            TaxCategoryDraft taxCategoryDraft = Helper.GetTestTaxCategoryDraft(_project);
            Task<Response<TaxCategory>> taxCategoryTask = client.TaxCategories().CreateTaxCategoryAsync(taxCategoryDraft);
            taxCategoryTask.Wait();

            _taxCategory = taxCategoryTask.Result.Result;
            Console.WriteLine("Tax Category '{0}' is created.", _taxCategory.Name);
            return _taxCategory;
        }

        // TODO 2.3. Create a Product
		/// <summary>
		/// Creates a new Product.
		/// </summary>
		/// <param name="productDraft">ProductDraft</param>
		/// <returns>Product</returns>
		/// <see href="http://dev.commercetools.com/http-api-projects-products.html#create-product"/>
        public Product CreateProductAsync(Client client)
		{
            ProductDraft productDraft = Helper.GetTestProductDraft(_project, _productType.Id, _taxCategory.Id);
            Task<Response<Product>> productTask = client.Products().CreateProductAsync(productDraft);
            productTask.Wait();

            _product = productTask.Result.Result;
            Console.WriteLine("A new product with id '{0}' is created.", _product.Id);

            return _product;
		}

        //TODO 2.4. Create a Catgeory (Optional)
        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="client">Client.</param>
        private Category CreateCategory(Client client)
        {
            CategoryDraft categoryDraft = Helper.GetTestCategoryDraft(_project);
            Response<Category> categoryResponse =
                client.Categories().CreateCategoryAsync(categoryDraft).Result;
            _category = categoryResponse.Result;
            Console.WriteLine("Category '{0}' is created.", _category.Name.GetValue("en"));
            return _category;
        }

        //TODO 2.5. Update a Product
        /// <summary>
        /// Udpates the product async: sets a key and changes the slug.
        /// </summary>
        /// <returns>The product async.</returns>
        /// <param name="client">Client.</param>
        private Product UdpateProductAsync(Client client)
        {
            SetKeyAction setKeyAction = new SetKeyAction();
            setKeyAction.Key = Helper.GetRandomString(15);

            GenericAction changeSlugAction = new GenericAction("changeSlug");
            LocalizedString newSlug = new LocalizedString();
            foreach (string language in _project.Languages)
            {
                newSlug.SetValue(language, string.Concat("updated-product-", language, "-", Helper.GetRandomString(10)));
            }
            changeSlugAction.SetProperty("slug", newSlug);
            //changeSlugAction.SetProperty("staged", true);

            List<UpdateAction> actions = new List<UpdateAction>();
            actions.Add(setKeyAction);
            actions.Add(changeSlugAction);

            Response<Product> response = client.Products()
                                               .UpdateProductAsync(
                                                   _product, 
                                                   actions
                                                  ).Result;
            _product = response.Result;
            Console.WriteLine("Product with id '{0}' has been updated.", _product.Id);
            return _product;
        }

        //TODO 2.6. Add a category to a product (optional)
        /// <summary>
        /// Adds the product to category.
        /// </summary>
        /// <returns>The product.</returns>
        /// <param name="client">Client.</param>
        /// <param name="category">Category.</param>
        private Product AddProductToCategory(Client client){
            
            Reference categoryReference = new Reference();
            categoryReference.ReferenceType = Common.ReferenceType.Category;
            categoryReference.Id = _category.Id;

            AddToCategoryAction addToCategoryAction = new AddToCategoryAction(categoryReference);

            Response<Product> productResponse =
                client.Products().UpdateProductAsync(_product, addToCategoryAction).Result;
            _product = productResponse.Result;
            Console.WriteLine("Product with ID {0} added to category with ID {1}.",
                              _product.Id,
                              _category.Id);
            return _product;
        }

        //TODO 2.7. Query all products
        /// <summary>
        /// Queries all products.
        /// </summary>
        /// <returns>all products.</returns>
        /// <param name="client">Client.</param>
        private async Task QueryAllProducts(Client client)
        {
            Response<ProductQueryResult> response = await client.Products().QueryProductsAsync(limit:500);

            if (response.Success)
            {
                ProductQueryResult productQueryResult = response.Result;
                Console.WriteLine("Product count: {0}", productQueryResult.Count);
                Console.WriteLine("Product total: {0}", productQueryResult.Total);
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

        //TODO 3.1. Create a Cart
        /// <summary>
        /// Creates the cart.
        /// </summary>
        /// <returns>The cart.</returns>
        /// <param name="client">Client.</param>
        private Cart CreateCart(Client client)
        {
            CartDraft cartDraft = Helper.GetTestCartDraft(_project);
            Task<Response<Cart>> cartTask = client.Carts().CreateCartAsync(cartDraft);
            //cartTask.Wait();

            _cart = cartTask.Result.Result;
            Console.WriteLine("Created new cart with ID {0}.", _cart.Id);

            return _cart;
        }

        //TODO 3.2. Add a line item to a cart
        /// <summary>
        /// Adds the line item to cart.
        /// </summary>
        /// <returns>The updated cart.</returns>
        /// <param name="product">Product.</param>
        /// <param name="cart">Cart.</param>
        private Cart AddLineItemToCart(Product product, Cart cart)
        {
            int masterVariantId = product.MasterData.Current.MasterVariant.Id;
            AddLineItemAction addLineItemAction = new AddLineItemAction(product.Id, masterVariantId);
            addLineItemAction.Quantity = 1;

            Response<Cart> cartResponse = _client.Carts().UpdateCartAsync(cart, addLineItemAction).Result;
            Console.WriteLine("Product with ID {0} has been added to cart with ID {1}.", product.Id, cart.Id);

            return cartResponse.Result;
        }

        //TODO 3.3. Remove a line item from a cart (optional)
        /// <summary>
        /// Removes the line item from cart.
        /// </summary>
        /// <returns>The cart.</returns>
        /// <param name="cart">Cart.</param>
        /// <param name="lineItemIndex">Line item index.</param>
        private Cart RemoveLineItemfromCart(Cart cart, int lineItemIndex)
        {
            String lineItemId = cart.LineItems[lineItemIndex].Id;
            RemoveLineItemAction removeLineItemAction =
                new RemoveLineItemAction(lineItemId);
            Response<Cart> cartResponse = _client.Carts().UpdateCartAsync(cart, removeLineItemAction).Result;

            Console.WriteLine("Item with ID {0} has been removed from cart with ID {1}.",
                              lineItemId, cart.Id);
            return cartResponse.Result;
        }

        //TODO 3.4. Create an Order from a Cart
        /// <summary>
        /// Creates the order from cart.
        /// </summary>
        /// <returns>The created order.</returns>
        /// <param name="cart">Cart.</param>
        private Order CreateOrderFromCart(Cart cart)
        {
            OrderFromCartDraft orderFromCartDraft = Helper.GetTestOrderFromCartDraft(cart);
            Response<Order> orderResponse = _client.Orders().CreateOrderFromCartAsync(orderFromCartDraft).Result;

            _order = orderResponse.Result;
            Console.WriteLine("Order with ID {0} is created from cart with ID {1}.", _order.Id, _cart.Id);
            return _order;
        }

        //TODO 3.5. Finilize an order.
        /// <summary>
        /// Finalizes the order.
        /// </summary>
        /// <returns>The updated order.</returns>
        /// <param name="order">Order.</param>
        private Order FinalizeOrder(Order order)
        {
            ChangeOrderStateAction changeOrderStateAction = new ChangeOrderStateAction(OrderState.Confirmed);

            String newOrderNumber = DateTime.Now.ToString();
            GenericAction setOrderNumberAction = new GenericAction("setOrderNumber");
            setOrderNumberAction.SetProperty("orderNumber", newOrderNumber);

            List<UpdateAction> actions = new List<UpdateAction>(){
                changeOrderStateAction, setOrderNumberAction
            };

            Response<Order> orderResponse = _client.Orders().UpdateOrderAsync(order, actions).Result;
            _order = orderResponse.Result;
            Console.WriteLine("The order with the ID {0} has been updated.", _order.Id);

            return _order;
        }

        //TODO 3.6. Delete a cart
        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cart">Cart.</param>
        private void DeleteCart(Cart cart)
        {
            Response<Cart> responseCart = _client.Carts().DeleteCartAsync(cart.Id, cart.Version).Result;
            Console.WriteLine("Cart with ID {0} is deleted.", cart.Id);
        }

        //TODO 3.7. Delete a product(optional)
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="product">Product.</param>
        private void DeleteProduct(Client client, Product product)
        {
            Response<Product> response = client.Products().DeleteProductAsync(product.Id, product.Version).Result;
            if (response.Success)
            {
                Console.WriteLine("Product with id '{0}' has been deleted successfully", product.Id);
            }
        }

        //TODO Get Messages (optional)
        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="client">Client.</param>
        private List<Message> GetMessages(Client client){
            Response<MessageQueryResult> response = client.Messages().QueryMessagesAsync().Result;
            MessageQueryResult messageQueryResult = response.Result;
            int? messageCount = messageQueryResult.Count;
            Console.WriteLine("Message count: {0}", messageCount);

            if (messageCount > 0)
            {
                Console.WriteLine("Messages: ");
                int x = 0;

                foreach (Message message in response.Result.Results)
                {
                    if (message != null)
                    {
                        x++;
                        Console.WriteLine("{0}: {1} \t {2}", x, message.Type, message.CreatedAt);
                    }
                    else{
                        Console.WriteLine("null");
                    }
                }
            }
            return response.Result.Results;
        }

    }
}