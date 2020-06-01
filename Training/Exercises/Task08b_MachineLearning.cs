using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.GraphQL;
using Training.GraphQL;
using Training.MachineLearningExtensions;

namespace Training
{
    /// <summary>
    /// MachineLearning Exercise
    /// </summary>
    public class Task08B : IExercise
    {
        private readonly IClient _client;
        
        public Task08B(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._client = clients.FirstOrDefault(c => c.Name == "MachineLearningClient");// the machine learning client
        }

        public async Task ExecuteAsync()
        {
            // Get categories recommendations using product name
            var additionalParams = new GetGeneralCategoriesRecommendationsAdditionalParameters()
            {
                ProductName = "black car"
            };
            var recommendationCommand = new QueryCommand<GeneralCategoryRecommendation>(additionalParams);

            var returnedSet = await _client.ExecuteAsync(recommendationCommand);
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

            var returnedSet2 = await _client.ExecuteAsync(recommendationCommand2);
            Console.WriteLine("Category Recommendations using Product Image Url:");
            foreach (var categoryRecommendation in returnedSet2.Results)
            {
                Console.WriteLine($"Category name: {categoryRecommendation.CategoryName}" +
                                  $", Confidence : {categoryRecommendation.Confidence}");
            }
        }
    }
}