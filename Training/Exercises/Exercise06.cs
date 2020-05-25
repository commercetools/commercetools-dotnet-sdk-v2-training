using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.ShippingMethods;
using commercetools.Sdk.Domain.TaxCategories;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// GET the project information
    /// GET By Id, Get By Key commands
    /// GET By Id, Get By Key builder
    /// </summary>
    public class Exercise06 : IExercise
    {
        private readonly IClient _client;
        
        public Exercise06(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._client = clients.FirstOrDefault(c => c.Name == Settings.DEFAULTCLIENT);// the default client
        }
        public async Task ExecuteAsync()
        {
            //await ExecuteByCommands();
            await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            // TODO: GET the project name
            var project =  await _client.ExecuteAsync(new GetProjectCommand());
            Console.WriteLine(project.Name);
            
            // TODO: GET a tax category via key
            var taxCategory = await _client.ExecuteAsync(new GetByKeyCommand<TaxCategory>("standard"));
            Console.WriteLine($"taxCategoryId: {taxCategory?.Id}");
            
            // TODO: GET a shipping method via id
            var shippingMethod = await _client.ExecuteAsync(new GetByIdCommand<ShippingMethod>("a3e20176-4fd0-4f7f-80ad-c3bdc686743d"));
            Console.WriteLine($"shippingMethod name: {shippingMethod?.Name}");
        }
        
        private async Task ExecuteByBuilder()
        {
            var taxCategory = await _client
                                            .Builder()
                                            .TaxCategories()
                                            .GetByKey("standard")
                                            .ExecuteAsync();
            Console.WriteLine($"taxCategoryId: {taxCategory?.Id}");

            var shippingMethod = await _client
                                            .Builder()
                                            .ShippingMethods()
                                            .GetById("a3e20176-4fd0-4f7f-80ad-c3bdc686743d")
                                            .ExecuteAsync();
            Console.WriteLine($"shippingMethod name: {shippingMethod?.Name}");
        }
    }
}
