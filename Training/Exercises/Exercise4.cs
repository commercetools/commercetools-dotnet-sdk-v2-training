using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Search for Products Exercise
    /// </summary>
    public class Exercise4 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise4(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            SearchForProductsByProductType();
        }

        private void SearchForProductsByProductType()
        {
            QueryCommand<Product> queryCommand = new QueryCommand<Product>();
            QueryPredicate<Product> queryPredicate = new QueryPredicate<Product>(p => p.ProductType.Id == Settings.PRODUCTTYPEID);
            queryCommand.SetWhere(queryPredicate);
            PagedQueryResult<Product> returnedSet = _commercetoolsClient.ExecuteAsync(queryCommand).Result;
            if (returnedSet.Results.Count > 0)
            {
                Console.WriteLine("------------------------------------------------------------");//as delimiter
                Console.WriteLine("Specific Products: ");
                foreach (var product in returnedSet.Results)
                {
                    Console.WriteLine(product.MasterData.Current.Name["en"]);
                }
            }
        }
    }
}