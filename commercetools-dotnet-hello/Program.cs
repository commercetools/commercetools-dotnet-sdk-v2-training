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
                "", // projectKey
                "", // clientID
                "", // clientSecret
                ProjectScope.ManageProject);

            // SHOP SIDE ////

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

            return null;
        }

        //TODO 2.2. Create a Tax Category
        /// <summary>
        /// Creates the tax category.
        /// </summary>
        /// <returns>The tax category.</returns>
        /// <param name="client">Client.</param>
        private TaxCategory CreateTaxCategory(Client client)
        {

            return null;
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

            return null;
		}

        //TODO 2.4. Create a Catgeory (Optional)
        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="client">Client.</param>
        private Category CreateCategory(Client client)
        {

            return null;
        }

        //TODO 2.5. Update a Product
        /// <summary>
        /// Udpates the product async: sets a key and changes the slug.
        /// </summary>
        /// <returns>The product async.</returns>
        /// <param name="client">Client.</param>
        private Product UdpateProductAsync(Client client)
        {

            return null;
        }

        //TODO 2.6. Add a category to a product (optional)
        /// <summary>
        /// Adds the product to category.
        /// </summary>
        /// <returns>The product.</returns>
        /// <param name="client">Client.</param>
        /// <param name="category">Category.</param>
        private Product AddProductToCategory(Client client){

            return null;
        }

        //TODO 2.7. Query all products
        /// <summary>
        /// Queries all products.
        /// </summary>
        /// <returns>all products.</returns>
        /// <param name="client">Client.</param>
        private async Task QueryAllProducts(Client client)
        {

        }

        //TODO 3.1. Create a Cart
        /// <summary>
        /// Creates the cart.
        /// </summary>
        /// <returns>The cart.</returns>
        /// <param name="client">Client.</param>
        private Cart CreateCart(Client client)
        {

            return null;
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

            return null;
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

            return null;
        }

        //TODO 3.4. Create an Order from a Cart
        /// <summary>
        /// Creates the order from cart.
        /// </summary>
        /// <returns>The created order.</returns>
        /// <param name="cart">Cart.</param>
        private Order CreateOrderFromCart(Cart cart)
        {

            return null;
        }

        //TODO 3.5. Finilize an order.
        /// <summary>
        /// Finalizes the order.
        /// </summary>
        /// <returns>The updated order.</returns>
        /// <param name="order">Order.</param>
        private Order FinalizeOrder(Order order)
        {

            return null;
        }

        //TODO 3.6. Delete a cart
        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cart">Cart.</param>
        private void DeleteCart(Cart cart)
        {

        }

        //TODO 3.7. Delete a product(optional)
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="product">Product.</param>
        private void DeleteProduct(Client client, Product product)
        {

        }

        //TODO Get Messages (optional)
        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="client">Client.</param>
        private List<Message> GetMessages(Client client){

            return null;
        }

    }
}