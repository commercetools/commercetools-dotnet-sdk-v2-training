using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using System.Text.Json;
using commercetools.Sdk.ImportApi.Models.Importcontainers;
using Training.Services;
using Newtonsoft.Json;

namespace Training
{
    public class Task03B : IExercise
    {
        private readonly IClient _importClient;
        private readonly ImportService _importService;
        private const string _containerKey = "nd-import-products-container";
        public Task03B(IEnumerable<IClient> clients)
        {
            _importClient = clients.FirstOrDefault(c => c.Name.Equals("ImportApiClient"));
            _importService = new ImportService(_importClient, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var csvFile = "Resources/products.csv";

            // CREATE importContainer
            //var container = await _importService.CreateImportContainer(
            //        new ImportContainerDraft {
            //            Key = _containerKey
            //        }
            //    );

            // Console.WriteLine($"ImportContainer created with key: {container.Key}");

            //  IMPORT products
            //var importResponse = await _importService.ImportProducts(_containerKey, csvFile);
            //Console.WriteLine($"Import ProductsDraft operation has been created, operation status count: {importResponse.OperationStatus.Count}");

            //foreach (ImportOperationStatus operationStatus in importResponse.OperationStatus) Console.WriteLine(operationStatus.OperationId);

            // GET import summary for the container
            var importSummary = await _importService.GetImportContainerSummary(_containerKey);
            Console.WriteLine(JsonConvert.SerializeObject(importSummary,Formatting.Indented));

            // GET operation status updates
            var operations = await _importService.GetImportOperationsByImportContainer(_containerKey, true);
            Console.WriteLine(JsonConvert.SerializeObject(operations,Formatting.Indented));

            // CHECK operation status by id
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
            // commercetools/commercetools-project-sync:5.3.1 - s all