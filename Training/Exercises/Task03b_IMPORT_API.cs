using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.ImportApi.Models.Common;
using commercetools.ImportApi.Models.Importsinks;
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
            var sinkKey = $"productsImport{Settings.RandomInt()}";
            var csvFile = "Resources/products.csv";

            var importSink = await _importService.CreateImportSink(new ImportSinkDraft
            {
                Key = sinkKey,
                ResourceType = IImportResourceType.ProductDraft
            });
            
            Console.WriteLine($"ImportSink created with key: {importSink.Key}");

            var importResponse = await _importService.ImportProducts(sinkKey, csvFile);
            Console.WriteLine($"Import ProductsDraft operation has been created, operation status count: {importResponse.OperationStatus.Count}");
            importResponse.OperationStatus.ForEach(o => Console.WriteLine(o.OperationId));
            var opertaion1 = "";
            var opertaion2 = "";

            /*
            var importOperation1 = await _importService.CheckImportOperationStatus(sinkKey, opertaion1);
            var importOperation2 = await _importService.CheckImportOperationStatus(sinkKey, opertaion2);
            Console.WriteLine($"Operation {opertaion1} : {importOperation1.State.JsonName}");
            Console.WriteLine($"Operation {opertaion2} : {importOperation2.State.JsonName}");
            */
        }
    }
}