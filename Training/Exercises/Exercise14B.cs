using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Search for Products By ProductTypeId Exercise
    /// </summary>
    public class Exercise14B : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise14B(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            try
            {
                ProductType productType = _commercetoolsClient
                    .ExecuteAsync(new GetByKeyCommand<ProductType>(Settings.PRODUCTTYPEKEY)).Result;

                QueryCommand<Product> queryCommand = new QueryCommand<Product>();
                queryCommand.Where(p => p.ProductType.Id == productType.Id.valueOf());//using QueryCommand Extensions
                PagedQueryResult<Product> returnedSet = await _commercetoolsClient.ExecuteAsync(queryCommand);
                if (returnedSet.Results.Count > 0)
                {
                    Console.WriteLine("Specific Products: ");
                    foreach (var product in returnedSet.Results)
                    {
                        Console.WriteLine(product.MasterData.Current.Name["en"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}