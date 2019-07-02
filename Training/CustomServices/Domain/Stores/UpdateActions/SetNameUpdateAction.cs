using commercetools.Sdk.Domain;

namespace Training.CustomServices.Domain.Stores.UpdateActions
{
    public class SetNameUpdateAction : UpdateAction<Store>
    {
        public string Action => "setName";
        public LocalizedString Name { get; set; }
    }
}
