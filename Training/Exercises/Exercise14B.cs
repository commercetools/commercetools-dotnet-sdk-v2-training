using System;
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
        public void Execute()
        {
            SearchForProductsByProductType();
            //SearchForProductsByProductTypeKey();
        }

        private void SearchForProductsByProductType()
        {
            QueryCommand<Product> queryCommand = new QueryCommand<Product>();
            QueryPredicate<Product> queryPredicate = new QueryPredicate<Product>(p => p.ProductType.Id == Settings.PRODUCTTYPEID);
            queryCommand.SetWhere(queryPredicate);
            PagedQueryResult<Product> returnedSet = _commercetoolsClient.ExecuteAsync(queryCommand).Result;
            if (returnedSet.Results.Count > 0)
            {
                Console.WriteLine("Specific Products: ");
                foreach (var product in returnedSet.Results)
                {
                    Console.WriteLine(product.MasterData.Current.Name["en"]);
                }
            }
        }
        /// <summary>
        /// Search for Products using QueryCommand Extensions (instead of creating QueryPredicate)
        /// </summary>
        private void SearchForProductsByProductTypeKey()
        {
            ProductType productType = _commercetoolsClient
                .ExecuteAsync(new GetByKeyCommand<ProductType>(Settings.PRODUCTTYPEKEY)).Result;

            QueryCommand<Product> queryCommand = new QueryCommand<Product>();
            queryCommand.Where(p => p.ProductType.Id == productType.Id.valueOf().ToString());
            PagedQueryResult<Product> returnedSet = _commercetoolsClient.ExecuteAsync(queryCommand).Result;
            if (returnedSet.Results.Count > 0)
            {
                Console.WriteLine("Specific Products: ");
                foreach (var product in returnedSet.Results)
                {
                    Console.WriteLine(product.MasterData.Current.Name["en"]);
                }
            }
        }
    }
}
