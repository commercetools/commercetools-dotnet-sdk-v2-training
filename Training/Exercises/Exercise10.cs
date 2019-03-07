using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.ProductProjections;
using commercetools.Sdk.Domain.Products.Attributes;
using commercetools.Sdk.Domain.Query;

namespace Training
{
    /// <summary>
    /// Query Products Exercise using Product Projections Search
    /// Search By color attribute
    /// </summary>
    public class Exercise10 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise10(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
           SearchForProductsByColor();
        }

        
        private void SearchForProductsByColor()
        {
            Filter<ProductProjection> redColorFilter = new Filter<ProductProjection>(p => p.Variants.Any(v => v.Attributes.Any(a => a.Name == "color" && ((TextAttribute)a).Value == "Red")));
            ProductProjectionSearchParameters searchParameters = new ProductProjectionSearchParameters();
            searchParameters.SetFilterQuery(new List<Filter<ProductProjection>>() { redColorFilter });
        
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;
            
            SearchProductProjectionsCommand searchProductProjectionsCommand = new SearchProductProjectionsCommand(searchParameters, stagedAdditionalParameters);
            PagedQueryResult<ProductProjection> searchResults = _commercetoolsClient.ExecuteAsync(searchProductProjectionsCommand).Result;
        
            foreach (var product in searchResults.Results)
            {
                Console.WriteLine(product.Name["en"]);
            }
        }
    }
}