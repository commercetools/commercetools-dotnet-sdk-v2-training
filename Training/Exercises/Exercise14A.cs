using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;

namespace Training
{
    /// <summary>
    /// Query Products Exercise
    /// </summary>
    public class Exercise14A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise14A(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            try
            {
                QueryCommand<Product> queryCommand = new QueryCommand<Product>();
                PagedQueryResult<Product> returnedSet = await _commercetoolsClient.ExecuteAsync(queryCommand);
                if (returnedSet.Results.Count > 0)
                {
                    Console.WriteLine("Products: ");
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
