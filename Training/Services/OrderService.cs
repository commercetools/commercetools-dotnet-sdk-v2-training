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
        /// Get an Order with Id
        /// </summary>
        /// <param name="Order Id"></param>
        /// <returns></returns>
        public async Task<IOrder> GetOrderById(string orderId)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithId(orderId)
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
                        Version = cart.Version

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
                .WithId(order.Id)
                .Post(orderUpdate)
                .ExecuteAsync();
        }

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
                .WithId(order.Id)
                .Post(orderUpdate)
                .WithExpand("state")
                .ExecuteAsync();
        }
        
    }
}