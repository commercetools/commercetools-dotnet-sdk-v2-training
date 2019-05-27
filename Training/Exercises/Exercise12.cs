using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task ExecuteAsync()
        {
            // Get categories recommendations using product name
            var additionalParams = new GetGeneralCategoriesRecommendationsAdditionalParameters()
            {
                ProductName = "car"
            };
            var recommendationCommand = new QueryCommand<GeneralCategoryRecommendation>(additionalParams);

            PagedQueryResult<GeneralCategoryRecommendation> returnedSet = await _machineLearningClient.ExecuteAsync(recommendationCommand);
            Console.WriteLine("Category Recommendations using Product Name:");
            foreach (var categoryRecommendation in returnedSet.Results)
            {
                Console.WriteLine($"Category name: {categoryRecommendation.CategoryName}, Confidence : {categoryRecommendation.Confidence}");
            }

            // Get categories recommendations using product image url

            var additionalParams2 = new GetGeneralCategoriesRecommendationsAdditionalParameters()
            {
                ProductImageUrl = "https://storage.googleapis.com/ctp-playground-ml-public/hoodie.jpg"
            };
            var recommendationCommand2 = new QueryCommand<GeneralCategoryRecommendation>(additionalParams2);

            PagedQueryResult<GeneralCategoryRecommendation> returnedSet2 = await _machineLearningClient.ExecuteAsync(recommendationCommand2);
            Console.WriteLine("Category Recommendations using Product Image Url:");
            foreach (var categoryRecommendation in returnedSet2.Results)
            {
                Console.WriteLine($"Category name: {categoryRecommendation.CategoryName}, Confidence : {categoryRecommendation.Confidence}");
            }
        }
    }
}
