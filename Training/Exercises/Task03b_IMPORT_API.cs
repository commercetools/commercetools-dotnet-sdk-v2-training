using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using System.Text.Json;
using commercetools.Sdk.ImportApi.Models.Importcontainers;
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

            //foreach (ImportOperationStatus operationStatus in importResponse.OperationStatus) Console.WriteLine(operationStatus.OperationId);

            // TODO: GET import summary for the container
            //    var importSummary = await _importService.GetImportContainerSummary(containerKey);
            //    Console.WriteLine(JsonConvert.SerializeObject(importSummary,Formatting.Indented));

            // TODO: GET operation status updates
            //    var operations = await _importService.GetImportOperationsByImportContainer(containerKey,true);
            //    Console.WriteLine(JsonConvert.SerializeObject(operations,Formatting.Indented));

            // TODO: CHECK operation status by id
            //     var opertaionId = "";

            //     var op = await _importService.CheckImportOperationStatus(opertaionId);
            //     Console.WriteLine($"Operation {opertaionId} : {op.State}");
        }
    }
}

            // Project-Sync Tool
            // https://github.com/commercetools/commercetools-project-sync#run
            //docker run \
            // -e SOURCE_PROJECT_KEY = xxx \
            // -e SOURCE_CLIENT_ID = xxx \
            // -e SOURCE_CLIENT_SECRET = xxx \
            // -e TARGET_PROJECT_KEY = xxx \
            // -e TARGET_CLIENT_ID = xxx \
            // -e TARGET_CLIENT_SECRET = xxx \
            // commercetools/commercetools-project-sync:5.2.2 - s all