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
    /// Delete Order
    /// </summary>
    public class Exercise8B : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise8B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            DeleteOrderByOrderNumber();
        }
        
        /// <summary>
        /// Delete Order BY OrderNumber 
        /// </summary>
        private void DeleteOrderByOrderNumber()
        {
            //retrieve order by ordernumber to get it's version
            Order retrievedOrder = _commercetoolsClient
                .ExecuteAsync(new GetOrderByOrderNumberCommand(Settings.ORDERNUMBER)).Result;

            retrievedOrder = _commercetoolsClient
                .ExecuteAsync(new DeleteByOrderNumberCommand(retrievedOrder.OrderNumber, retrievedOrder.Version)).Result;
            
            Console.WriteLine($"Order Number: {retrievedOrder.OrderNumber} has been deleted");
        }
        
        /// <summary>
        /// Delete Order BY OrderId 
        /// </summary>
        private void DeleteOrderById()
        {
            //retrieve order by ordernumber to get it's version
            Order retrievedOrder = _commercetoolsClient
                .ExecuteAsync(new GetOrderByOrderNumberCommand(Settings.ORDERNUMBER)).Result;

            retrievedOrder = _commercetoolsClient
                .ExecuteAsync(new DeleteByIdCommand<Order>(new Guid(retrievedOrder.Id), retrievedOrder.Version)).Result;
            
            Console.WriteLine($"Order Number: {retrievedOrder.OrderNumber} has been deleted");
        }
    }
}