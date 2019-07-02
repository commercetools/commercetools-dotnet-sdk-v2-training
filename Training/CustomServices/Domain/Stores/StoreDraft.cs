using commercetools.Sdk.Domain;

namespace Training.CustomServices.Domain.Stores
{
    public class StoreDraft : IDraft<Store>
    {
        public string Key { get; set; }
        public LocalizedString Name { get; set; }
    }
}
