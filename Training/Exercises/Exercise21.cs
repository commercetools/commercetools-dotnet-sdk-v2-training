using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.CustomObjects;

namespace Training
{
    /// <summary>
    /// Create a Custom Object Exercise
    /// </summary>
    public class Exercise21 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise21(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //Create CustomObject of type FooBar
            var fooBar = new FooBar();
            CustomObjectDraft<FooBar> draft = new CustomObjectDraft<FooBar>
            {
                Container = "FooBarContainer",
                Key = "Key",
                Value = fooBar
            };
            //Creates a new custom object or updates an existing custom object.
            //If an object with the given container/key exists, the object will be replaced with the new value and the version is incremented
            var customObject = await _commercetoolsClient
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
