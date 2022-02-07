using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.ImportApi.Models.Importcontainers;
using Newtonsoft.Json;
using Training.Services;

namespace Training
{
    public class Task03B : IExercise
    {
        private readonly IClient _importClient;
        private readonly ImportService _importService;
        private const string containerKey = "mg1-import-container";
        public Task03B(IEnumerable<IClient> clients)
        {
            _importClient = clients.FirstOrDefault(c => c.Name.Equals("ImportApiClient"));
            _importService = new ImportService(_importClient, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var csvFile = "Resources/products.csv";

            var importContainer = await _importService.CreateImportContainer(
                new ImportContainerDraft { Key = containerKey }
            );
            
            Console.WriteLine($"ImportContainer created with key: {importContainer.Key}");

            var importResponse = await _importService.ImportProducts(containerKey, csvFile);
            Console.WriteLine($"Import ProductsDraft operation has been created, operation status count: {importResponse.OperationStatus.Count}");
            importResponse.OperationStatus.ForEach(o => Console.WriteLine(o.OperationId)); 
           
            var importSummary = await _importService.GetImportContainerSummary(containerKey);
            Console.WriteLine(JsonConvert.SerializeObject(importSummary,Formatting.Indented));

            var operations = await _importService.GetImportOperationsByImportContainer(containerKey,true);  
            Console.WriteLine(JsonConvert.SerializeObject(operations,Formatting.Indented));
            
            // var opertaionId = "a8ed805b-3a42-40f7-807c-3e161b9b3015";
            
            // var op = await _importService.CheckImportOperationStatus(opertaionId);
            // Console.WriteLine($"Operation {opertaionId} : {op.State}");
        }
    }
}