using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Types;
using commercetools.Base.Client;
using Type = System.Type;

namespace Training
{
    /// <summary>
    /// 1- Create TypeDraft with Custom fields
    /// 2- Create The Type and assign it to customers (as Resources you want to extend)
    public class Task07A : IExercise
    {
        private readonly IClient _client;

        public Task07A(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            var typeDraft = this.CreatePlantCheckTypeDraft();
            var createdType = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Types()
                .Post(typeDraft)
                .ExecuteAsync();
            
            Console.WriteLine($"New custom type has been created with Id: {createdType.Id}");
        }

        public TypeDraft CreatePlantCheckTypeDraft()
        {
            var typeDraft = new TypeDraft
            {
                Key = "plantcheck-key",
                Name = new LocalizedString {{"en", "plantCheck custom Type"}},
                ResourceTypeIds = new List<IResourceTypeId> {IResourceTypeId.Customer},
                FieldDefinitions = new List<IFieldDefinition>
                {
                    this.CreatePlantCheckFieldDefinition()
                }
            };
            return typeDraft;
        }

        private FieldDefinition CreatePlantCheckFieldDefinition()
        {
            var fieldDefinition = new FieldDefinition
            {
                Name = "plantCheck",
                Required = false,
                Label = new LocalizedString {{"EN", "plantCheck"}, {"DE", "plantCheck"}},
                Type = new CustomFieldBooleanType()
            };
            return fieldDefinition;
        }
    }
}