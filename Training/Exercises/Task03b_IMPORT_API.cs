using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using System.Text.Json;
using commercetools.ImportApi.Models.Importcontainers;
using Training.Services;

namespace Training
{
    public class Task03B : IExercise
    {
        private readonly IClient _importClient;
        private readonly ImportService _importService;
        private const string containerKey = "";
        public Task03B(IEnumerable<IClient> clients)
        {
            _importClient = clients.FirstOrDefault(c => c.Name.Equals("ImportApiClient"));
            _importService = new ImportService(_importClient, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var csvFile = "Resources/products.csv";

            // TODO: CREATE importContainer
                
            // Console.WriteLine($"ImportContainer created with key: {importContainer.Key}");

            //  TODO: IMPORT products    
            //    Console.WriteLine($"Import ProductsDraft operation has been created, operation status count: {importResponse.OperationStatus.Count}");
            //    importResponse.OperationStatus.ForEach(o => Console.WriteLine(o.OperationId));
                
            // TODO: GET import summary for the container
            //    var importSummary = await _importService.GetImportContainerSummary(containerKey);
            //    Console.WriteLine(JsonSerializer.Serialize(importSummary,new JsonSerializerOptions(){WriteIndented = true}));

            // TODO: GET operation status updates
            //    var operations = await _importService.GetImportOperationsByImportContainer(containerKey,true);
            //    Console.WriteLine(JsonSerializer.Serialize(importSummary,new JsonSerializerOptions(){WriteIndented = true}));

            // TODO: CHECK operation status by id
            //     var opertaionId = "";

            //     var op = await _importService.CheckImportOperationStatus(opertaionId);
            //     Console.WriteLine($"Operation {opertaionId} : {op.State}");
        }
    }
}