using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.CustomObjects;
using commercetools.Sdk.Domain.Types;
using commercetools.Sdk.Domain.Types.FieldTypes;
using Type = commercetools.Sdk.Domain.Types.Type;


namespace Training
{
    /// <summary>
    /// Create a Custom Object Exercise
    /// </summary>
    public class Task07B : IExercise
    {
        private readonly IClient _client;
        
        public Task07B(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //Create CustomObject of type FooBar
            var fooBar = new FooBar();
            var draft = new CustomObjectDraft<FooBar>
            {
                Container = "FooBarContainer",
                Key = "Key",
                Value = fooBar
            };
            //Creates a new custom object or updates an existing custom object.
            //If an object with the given container/key exists, the object will be replaced with the new value and the version is incremented
            var customObject = await _client
                .ExecuteAsync(new CustomObjectUpsertCommand<FooBar>(draft));

            Console.WriteLine($"custom object created with Id {customObject.Id} with version {customObject.Version}");
        }
    }
    /// <summary>
    /// A demo class for a value of a custom object
    /// </summary>
    public class FooBar
    {
        public string Bar { get; set; }

        public FooBar()
        {
            this.Bar = "bar";
        }

        public FooBar(string bar)
        {
            this.Bar = bar;
        }
    }
}