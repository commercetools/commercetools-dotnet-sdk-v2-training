using System;
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

        public async Task<IOrder> GetOrderById(string orderId)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Orders()
                .WithId(orderId)
                .Get()
                .ExecuteAsync();
        }
        
    }
}