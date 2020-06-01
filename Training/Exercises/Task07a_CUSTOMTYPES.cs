using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Types;
using commercetools.Sdk.Domain.Types.FieldTypes;
using commercetools.Sdk.HttpApi.CommandBuilders;
using Type = commercetools.Sdk.Domain.Types.Type;


namespace Training
{
    /// <summary>
    /// Create your own resource type (like ShoeSize Type) exercise
    /// 1- Create TypeDraft with Custom fields
    /// 2- Create The Type and assign it to customers (as Resources you want to extend)
    public class Task07A : IExercise
    {
        private readonly IClient _client;

        public Task07A(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public TypeDraft CreateShoeSizeTypeDraft()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get ShoeSizeFieldDefinition
        /// </summary>
        /// <returns></returns>
        private FieldDefinition CreateShoeSizeFieldDefinition()
        {
            throw new NotImplementedException();
        }
    }
}