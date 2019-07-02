using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Common;

namespace Training.CustomServices.Domain.Stores
{
    [Endpoint("stores")]
    public class Store : Resource<Store>
    {
        public string Key { get; set; }
        public LocalizedString Name { get; set; }
    }
}
