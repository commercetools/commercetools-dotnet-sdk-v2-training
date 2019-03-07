using commercetools.Sdk.Domain;

namespace Training.MachineLearningExtensions
{
    public class GetGeneralCategoriesRecommendationsAdditionalParameters : IAdditionalParameters<GeneralCategoryRecommendation>
    {
        private const float CONFIDENCEMIN = 0.01f;
        private const float CONFIDENCEMAX = 1.0f;
        
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public float ConfidenceMin { get; set; }
        public float ConfidenceMax { get; set; }

        public GetGeneralCategoriesRecommendationsAdditionalParameters()
        {
            //set default for ConfidenceMin and ConfidenceMax
            this.ConfidenceMin = CONFIDENCEMIN;
            this.ConfidenceMax = CONFIDENCEMAX;
        }
    }
}