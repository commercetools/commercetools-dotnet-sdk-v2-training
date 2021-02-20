using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Base.Client;
using commercetools.ImportApi.Models.Common;
using commercetools.ImportApi.Models.Importoperations;
using commercetools.ImportApi.Models.Importrequests;
using commercetools.ImportApi.Models.Importsinks;
using commercetools.ImportApi.Models.Productdrafts;
using commercetools.ImportApi.Models.Productvariants;
using commercetools.Sdk.ImportApi.Extensions;

namespace Training.Services
{
    public class ImportService
    {
        private readonly IClient _importClient;
        private readonly CSVHelper _csvHelper;
        private readonly string _projectKey;
        private const string PREFIX = "MG";

        public ImportService(IClient client, string projectKey)
        {
            _importClient = client;
            _projectKey = projectKey;
            _csvHelper = new CSVHelper();
        }

        public async Task<ImportSink> CreateImportSink(ImportSinkDraft draft)
        {
            var importSink = await _importClient.WithImportApi().WithProjectKey(_projectKey)
                .ImportSinks()
                .Post(draft)
                .ExecuteAsync();
            return importSink;
        }

        public async Task<ImportOperation> CheckImportOperationStatus(string importSinkKey, string id)
        {
            var importOperation = await _importClient.WithImportApi().WithProjectKey(_projectKey)
                .ProductDrafts()
                .ImportSinkKeyWithImportSinkKeyValue(importSinkKey)
                .ImportOperations()
                .WithIdValue(id)
                .Get().ExecuteAsync();
            return importOperation;
        }

        public async Task<ImportResponse> ImportProducts(string importSinkKey, string csvFile)
        {
            var productDraftImportList = GetProductDraftImportList(csvFile);
            var productDraftImportRequest = new ProductDraftImportRequest()
            {
                Type = IImportResourceType.ProductDraft,
                Resources = productDraftImportList
            };
            var importResponse = await _importClient.WithImportApi().WithProjectKey(_projectKey)
                .ProductDrafts()
                .ImportSinkKeyWithImportSinkKeyValue(importSinkKey)
                .Post(productDraftImportRequest)
                .ExecuteAsync();
            return importResponse;
        }

        #region Helpers

        private List<IProductDraftImport> GetProductDraftImportList(string fileName)
        {
            var listOfCsvProducts = ParseCsvFile(fileName);
            var listOfProductDraftImport = new List<IProductDraftImport>();
            foreach (var product in listOfCsvProducts)
            {
                var draftImport = new ProductDraftImport
                {
                    Key = PREFIX + "-" + product.ProductName,
                    Name = new LocalizedString {{"en", product.ProductName} , {"de", product.ProductName}},
                    ProductType = new ProductTypeKeyReference {Key = product.ProductType},
                    Slug = new LocalizedString {{"en", PREFIX + "-" + product.ProductName}, {"de", PREFIX + "-" + product.ProductName}},
                    MasterVariant = new ProductVariantDraftImport
                    {
                        Sku = product.InventoryId,
                        Images = new List<IImage>
                        {
                            new Image {Url = product.ImageUrl, Dimensions = new AssetDimensions {W = 177, H = 237}}
                        },
                        Attributes = new List<IAttribute>
                        {
                            new TextAttribute { Name = "phonecolor", Value = product.Color },
                            new NumberAttribute { Name = "phoneweight", Value = product.Weight }
                        }
                    }
                };
                listOfProductDraftImport.Add(draftImport);
            }

            return listOfProductDraftImport;
        }

        private List<CSVProduct> ParseCsvFile(string file)
        {
            var list = _csvHelper.GetData<CSVProduct>(file);
            return list;
        }

        #endregion
    }

    public class CSVProduct
    {
        public string ProductType { get; set; }
        public string ProductId { get; set; }
        public string InventoryId { get; set; }
        public string ArticleId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string BasePrice { get; set; }
        public string CurrencyCode { get; set; }
        public string ImageUrl { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
    }
}