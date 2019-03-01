using System;
using System.Collections.Generic;
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
    public class Exercise8 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        private static Random random = new Random();
        
        public Exercise8(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            CreateAnOrderFromCart();
        }
        
        /// <summary>
        /// Create Order from Cart
        /// </summary>
        private void CreateAnOrderFromCart()
        {
            //Create Order Draft
            var orderFromCartDraft = this.GetOrderFromCartDraft();
            
            //Create Order
            Order order = _commercetoolsClient.ExecuteAsync(new CreateCommand<Order>(orderFromCartDraft)).Result;
            
            //Display Order Number
            Console.WriteLine($"Order Created with order number: {order.OrderNumber}");
            
        }
        /// <summary>
        /// Create Draft Order from Cart 
        /// </summary>
        /// <returns></returns>
        private OrderFromCartDraft GetOrderFromCartDraft()
        {
            string cartId = "200b3777-6373-437c-8327-4489b170f90b";
            
            //Get the cart By Id (Cart must have at least one product)
            Cart cart =
                _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Cart>(new Guid(cartId))).Result;
            
            //Then Create Order from this Cart
            OrderFromCartDraft orderFromCartDraft = new OrderFromCartDraft();
            orderFromCartDraft.Id = cart.Id;
            orderFromCartDraft.Version = cart.Version;
            orderFromCartDraft.OrderNumber = $"Order{random.Next()}";
            return orderFromCartDraft;
        }
    }
}