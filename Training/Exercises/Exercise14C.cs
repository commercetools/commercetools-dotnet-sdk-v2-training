using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.ProductProjections;
using commercetools.Sdk.Domain.Products.Attributes;

namespace Training
{
    /// <summary>
    /// Query Products Exercise using Product Projections Search
    /// Search By color attribute
    /// </summary>
    public class Exercise14C : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise14C(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        /// <summary>
        /// Search in Stage Products for products have attribute "color" and with value "Red"
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            SearchProductProjectionsCommand searchProductProjectionsCommand = new SearchProductProjectionsCommand();
            searchProductProjectionsCommand.SetStaged(true);
            searchProductProjectionsCommand.FilterQuery(p =>
                p.Variants.Any(v => v.Attributes.Any(a => a.Name == "color" && ((TextAttribute) a).Value == "Red")));
            PagedQueryResult<ProductProjection> searchResults = await _commercetoolsClient.ExecuteAsync(searchProductProjectionsCommand);
            if (searchResults.Results.Count > 0)
            {
                foreach (var product in searchResults.Results)
                {
                    Console.WriteLine(product.Name["en"]);
                }
            }
            else
            {
                Console.WriteLine("No Results found");
            }
        }
    }
}
