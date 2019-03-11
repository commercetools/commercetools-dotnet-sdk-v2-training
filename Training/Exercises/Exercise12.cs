using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.Query;
using Type = commercetools.Sdk.Domain.Type;

namespace Training
{
    /// <summary>
    /// Create your own resource type (like ShoeSize Type) exercise
    /// 1- Create TypeDraft with Custom fields
    /// 2- Create The Type and assign it to customers (as Resources you want to extend)
    /// </summary>
    public class Exercise12 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise12(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            CreateYourOwnResourceType();
        }

        private void CreateYourOwnResourceType()
        {
            TypeDraft typeDraft = this.CreateShoeSizeTypeDraft();
            Type createdType = _commercetoolsClient.ExecuteAsync(new CreateCommand<Type>(typeDraft)).Result;
            Console.WriteLine($"New custom type has been created with Id: {createdType.Id}");
            
        }
        public TypeDraft CreateShoeSizeTypeDraft()
        {
            TypeDraft typeDraft = new TypeDraft();
            typeDraft.Key = "shoe-size-key";
            typeDraft.Name = new LocalizedString();
            typeDraft.Name.Add("en", "Shoe Size Type");
            typeDraft.Description = new LocalizedString();
            typeDraft.Description.Add("en", "Store Customer's Shoe size");
            typeDraft.ResourceTypeIds = new List<ResourceTypeId>() { ResourceTypeId.Customer };
            typeDraft.FieldDefinitions = new List<FieldDefinition>();
            typeDraft.FieldDefinitions.Add(this.CreateShoeSizeFieldDefinition());
            return typeDraft;
        }
        private FieldDefinition CreateShoeSizeFieldDefinition()
        {
            FieldDefinition fieldDefinition = new FieldDefinition();
            fieldDefinition.Name = "shoe-size-field";
            fieldDefinition.Required = true;
            fieldDefinition.Label = new LocalizedString();
            fieldDefinition.Label.Add("en", "Shoe Size");
            fieldDefinition.Type = new NumberFieldType();
            return fieldDefinition;
        }
    }
}