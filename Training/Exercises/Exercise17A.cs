using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Orders;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Products;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Create an order from cart, Cart must have at least one product and has to be in active state
    /// </summary>
    public class Exercise17A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise17A(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //Create Order Draft
            var orderFromCartDraft = this.GetOrderFromCartDraft();

            //Create Order
            Order order = await _commercetoolsClient.ExecuteAsync(new CreateCommand<Order>(orderFromCartDraft));


            //Display Order Number
            Console.WriteLine($"Order Created with order number: {order.OrderNumber}, and Total Price: {order.TotalPrice.CentAmount} cents");
        }

        /// <summary>
        /// Create Draft Order from Cart
        /// </summary>
        /// <returns></returns>
        private OrderFromCartDraft GetOrderFromCartDraft()
        {
            //Get the active cart By Customer Id (Cart must have at least one product)
            Cart cart =
                _commercetoolsClient.ExecuteAsync(new GetCartByCustomerIdCommand(new Guid(Settings.CUSTOMERID))).Result;

            //Then Create Order from this Cart
            OrderFromCartDraft orderFromCartDraft = new OrderFromCartDraft();
            orderFromCartDraft.Id = cart.Id;
            orderFromCartDraft.Version = cart.Version;
            orderFromCartDraft.OrderNumber = $"Order{Settings.RandomInt()}";
            return orderFromCartDraft;
        }
    }
}
