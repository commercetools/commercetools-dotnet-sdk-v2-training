using System.Collections.Generic;
using commercetools.Sdk.Domain;
using commercetools.Sdk.HttpApi.AdditionalParameters;
using Type = System.Type;

namespace Training.MachineLearningExtensions
{
    public class GetGeneralCategoriesRecommendationsAdditionalParametersBuilder : IAdditionalParametersBuilder
    {
        public bool CanBuild(Type type)
        {
            return type == typeof(GetGeneralCategoriesRecommendationsAdditionalParameters);
        }

        public List<KeyValuePair<string, string>> GetAdditionalParameters(IAdditionalParameters additionalParameters)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            GetGeneralCategoriesRecommendationsAdditionalParameters recommendationsAdditionalParameters = additionalParameters as GetGeneralCategoriesRecommendationsAdditionalParameters;
            if (recommendationsAdditionalParameters == null)
            {
                return parameters;
            }

            //parameters.Add(new KeyValuePair<string, string>("confidenceMin", recommendationsAdditionalParameters.ConfidenceMin.ToString()));
            //parameters.Add(new KeyValuePair<string, string>("confidenceMax", recommendationsAdditionalParameters.ConfidenceMax.ToString()));
            
            if (!string.IsNullOrEmpty(recommendationsAdditionalParameters.ProductName))
            {
                parameters.Add(new KeyValuePair<string, string>("productName", recommendationsAdditionalParameters.ProductName));
            }
            if (!string.IsNullOrEmpty(recommendationsAdditionalParameters.ProductImageUrl))
            {
                parameters.Add(new KeyValuePair<string, string>("productImageUrl", recommendationsAdditionalParameters.ProductImageUrl));
            }

            return parameters;
        }
    }
}