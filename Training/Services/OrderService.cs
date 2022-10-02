using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Carts;
using commercetools.Sdk.Api.Models.Orders;
using commercetools.Sdk.Api.Models.States;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class OrderService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public OrderService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// Get an Order with order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public async Task<IOrder> GetOrderByOrderNumber(string orderNumber)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithOrderNumber(orderNumber)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// Create an Order From the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<IOrder> CreateOrder(ICart cart)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Change Order State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IOrder> ChangeOrderState(IOrder order, IOrderState state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Change Order Workflow State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IOrder> ChangeWorkflowState(IOrder order, IStateResourceIdentifier state)
        {
            throw new NotImplementedException();
        }
        
    }
}