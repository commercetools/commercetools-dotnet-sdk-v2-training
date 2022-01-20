using System;
using System.Threading.Tasks;
using commercetools.Api.Models.Carts;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class CartService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public CartService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        public async Task<ICart> CreateCart(ICartDraft cartDraft)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .Post(cartDraft)
                .ExecuteAsync();
        }

        public async Task<ICart> GetCartByKey(string cartKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Carts()
                .WithKey(cartKey)
                .Get()
                .ExecuteAsync();
        }
        
    }
}