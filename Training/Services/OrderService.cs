using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Carts;
using commercetools.Api.Models.Orders;
using commercetools.Api.Models.States;
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
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .Post(
                    new OrderFromCartDraft
                    {
                        Cart = new CartResourceIdentifier{Id = cart.Id},
                        Version = cart.Version,
                        OrderNumber = "HAPG" + Settings.RandomString()
                    }
                )
                .ExecuteAsync();
        }


        /// <summary>
        /// Change Order State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IOrder> ChangeOrderState(IOrder order, IOrderState state)
        {
            var orderUpdate = new OrderUpdate
            {
                Version = order.Version,
                Actions = new List<IOrderUpdateAction>
                {
                    new OrderChangeOrderStateAction {OrderState = state}
                }
            };

            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithOrderNumber(order.OrderNumber)
                .Post(orderUpdate)
                .ExecuteAsync();
        }
        /// <summary>
        /// Change Order Workflow State
        /// </summary>
        /// <param name="order"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<IOrder> ChangeWorkflowState(IOrder order, IStateResourceIdentifier state)
        {
            var orderUpdate = new OrderUpdate
            {
                Version = order.Version,
                Actions = new List<IOrderUpdateAction>
                {
                    new OrderTransitionStateAction() {State = state}
                }
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithOrderNumber(order.OrderNumber)
                .Post(orderUpdate)
                .WithExpand("state")
                .ExecuteAsync();
        }
        
    }
}