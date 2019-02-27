using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Query Products Exercise
    /// </summary>
    public class Exercise3 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise3(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            QueryAllProducts();
        }

        private void QueryAllProducts()
        {
            QueryCommand<Product> queryCommand = new QueryCommand<Product>();
            PagedQueryResult<Product> returnedSet = _commercetoolsClient.ExecuteAsync(queryCommand).Result;
            if (returnedSet.Results.Count > 0)
            {
                Console.WriteLine("Products: ");
                foreach (var product in returnedSet.Results)
                {
                    Console.WriteLine(product.MasterData.Current.Name["en"]);
                }
            }
        }
    }
}