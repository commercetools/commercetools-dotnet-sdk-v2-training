using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Types;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// 1- Create TypeDraft with Custom fields
    /// 2- Create The Type and assign it to customers (as Resources you want to extend)
    public class Task07A : IExercise
    {
        private readonly IClient _client;
        private readonly TypeService _typeService;

        public Task07A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _typeService = new TypeService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            // TODO: CREATE custom type with one field 'allowed-to-place-orders'
            
            // Console.WriteLine($"New custom type has been created with Id: {createdType.Id}");
        }
    }
}