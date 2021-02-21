using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Types;
using commercetools.Base.Client;

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
            //create type Draft
            var typeDraft = this.CreateTypeDraft();
            
            //create custom type
             
            
            //Console.WriteLine($"New custom type has been created with Id: {createdType.Id}");
        }

        public TypeDraft CreateTypeDraft()
        {
            var typeDraft = new TypeDraft
            {
                Key = "allowed-to-place-orders",
                Name = new LocalizedString {{"de", "allowed-to-place-orders"}},
                ResourceTypeIds = new List<IResourceTypeId> {IResourceTypeId.Customer},
                FieldDefinitions = new List<IFieldDefinition>
                {
                    this.CreateTypeFieldDefinition()
                }
            };
            return typeDraft;
        }

        private FieldDefinition CreateTypeFieldDefinition()
        {
            var fieldDefinition = new FieldDefinition
            {
                Name = "allowed-to-place-orders",
                Required = false,
                Label = new LocalizedString {{"de", "Allowed to place orders"}},
                Type = new CustomFieldBooleanType()
            };
            return fieldDefinition;
        }
    }
}