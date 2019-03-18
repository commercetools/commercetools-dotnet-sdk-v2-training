using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using Training.MachineLearningExtensions;

namespace Training
{
    /// <summary>
    /// Machine Learning Exercise
    /// </summary>
    public class Exercise12 : IExercise
    {
        private readonly IClient _machineLearningClient;

        public Exercise12(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._machineLearningClient = clients.FirstOrDefault(c => c.Name == Settings.MACHINELEARNINGCLIENT);// the machine learning client
        }
        public void Execute()
        {
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
            Console.WriteLine("Category Recommendations using Product Name:");
            foreach (var categoryRecommendation in returnedSet.Results)
            {
                Console.WriteLine($"Category name: {categoryRecommendation.CategoryName}, Confidence : {categoryRecommendation.Confidence}");
            }
        }
        private void GetCategoriesRecommendationsFromProductImageUrl()
        {
            var additionalParams = new GetGeneralCategoriesRecommendationsAdditionalParameters()
            {
                ProductImageUrl = "https://27f39057e2c520ef562d-e965cc5b4f2ea17c6cdc007c161d738e.ssl.cf3.rackcdn.com/Gar-HOkwvZ2E-small.jpg"
            };
            var recommendationCommand = new QueryCommand<GeneralCategoryRecommendation>(additionalParams);

            PagedQueryResult<GeneralCategoryRecommendation> returnedSet = _machineLearningClient.ExecuteAsync(recommendationCommand).Result;
            Console.WriteLine("Category Recommendations using Product Image Url:");
            foreach (var categoryRecommendation in returnedSet.Results)
            {
                Console.WriteLine($"Category name: {categoryRecommendation.CategoryName}, Confidence : {categoryRecommendation.Confidence}");
            }
        }
    }
}
