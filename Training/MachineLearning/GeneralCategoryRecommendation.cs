using commercetools.Sdk.Domain;

namespace Training.MachineLearningExtensions
{
    [Endpoint("recommendations/general-categories")]
    public class GeneralCategoryRecommendation
    {
        public string CategoryName { get; set; }
        public float Confidence { get; set; }
    }
}