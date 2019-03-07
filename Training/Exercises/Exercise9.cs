using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;
using Training.MachineLearningExtensions;

namespace Training
{
    /// <summary>
    /// Machine Learning Exercise
    /// </summary>
    public class Exercise9 : IExercise
    {
        private readonly IClient _machineLearningClient;
        
        public Exercise9(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._machineLearningClient = clients.FirstOrDefault(c => c.Name == Settings.MACHINELEARNINGCLIENT);// the machine learning client
        }
        public void Execute()
        {
            //first change the api base address of the client
            GetCategoriesRecommendationsFromProductName();
            GetCategoriesRecommendationsFromProductImageUrl();
        }

        private void GetCategoriesRecommendationsFromProductName()
        {
            var additionalParams = new GetGeneralCategoriesRecommendationsAdditionalParameters()
            {
                ProductName = "car"
            };
            var recommendationCommand = new QueryCommand<GeneralCategoryRecommendation>(additionalParams);
            
            PagedQueryResult<GeneralCategoryRecommendation> returnedSet = _machineLearningClient.ExecuteAsync(recommendationCommand).Result;
        }
        private void GetCategoriesRecommendationsFromProductImageUrl()
        {
            
        }
    }
}