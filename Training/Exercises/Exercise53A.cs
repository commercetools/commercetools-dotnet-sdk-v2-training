using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using Type = commercetools.Sdk.Domain.Type;

namespace Training
{
    public class Exercise53A : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise53A(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //create the fieldDefinition
            var fieldDefinitionName = "transactionWireCard";
            var localizedLabelString = new LocalizedString
            {
                {"en", "Transaction Info"},
                {"de", "Transaktionsinformation"}
            };
            var fieldDefinition = new FieldDefinition
            {
                Label = localizedLabelString,
                Name = fieldDefinitionName,
                Required = false,
                InputHint = TextInputHint.SingleLine,
                Type = new StringFieldType()
            };
            //put fields in a Definition list
            var fieldDefinitions = new List<FieldDefinition> { fieldDefinition };

            //create the new type
            var typeDraft = new TypeDraft
            {
                Name = new LocalizedString
                {
                    {"en", "payment-transaction-wirecard"},
                    {"de", "Transaktionsdaten WireCard"}
                },
                ResourceTypeIds = new List<ResourceTypeId>
                {
                    ResourceTypeId.PaymentInterfaceInteraction
                },
                FieldDefinitions = fieldDefinitions,
                Key = "WirecardPaymentTran-Key"
            };
            var wireCardPaymentType = await _commercetoolsClient.ExecuteAsync(new CreateCommand<Type>(typeDraft));
            Console.WriteLine($"WireCard Payment Type Created with ID: {wireCardPaymentType.Id}, with Key: {wireCardPaymentType.Key}");
        }
    }
}
