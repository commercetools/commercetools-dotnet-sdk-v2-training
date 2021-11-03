using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.ImportApi.Models.Importcontainers;
using Training.Services;

namespace Training
{
    public class Task03B : IExercise
    {
        private readonly IClient _importClient;
        private readonly ImportService _importService;
        public Task03B(IEnumerable<IClient> clients)
        {
            _importClient = clients.FirstOrDefault(c => c.Name.Equals("ImportApiClient"));
            _importService = new ImportService(_importClient, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var containerKey = $"productsImport{Settings.RandomInt()}";
            var csvFile = "Resources/products.csv";

            // Create importContainer
            
//            Console.WriteLine($"ImportContainer created with key: {importContainer.Key}");

            // Import Product Drafts from the csv file and get Operation Ids from the response
            

            /*
            var opertaion1 = "";
            var opertaion2 = "";
            var importOperation1 = await _importService.CheckImportOperationStatus(containerKey, opertaion1);
            var importOperation2 = await _importService.CheckImportOperationStatus(containerKey, opertaion2);
            Console.WriteLine($"Operation {opertaion1} : {importOperation1.State.JsonName}");
            Console.WriteLine($"Operation {opertaion2} : {importOperation2.State.JsonName}");
            */
        }
    }
}