using System;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;
using commercetools.Sdk.Domain.Stores;
using commercetools.Sdk.HttpApi.CommandBuilders;
using Type = commercetools.Sdk.Domain.Types.Type;

namespace Training
{
    /// <summary>
    /// Registers a new customer
    /// </summary>
    public class Exercise07 : IExercise
    {
        private readonly IClient _client;

        public Exercise07(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //await ExecuteByCommands();
            await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            var customerDraft = new CustomerDraft
            {
                FirstName = "fName",
                LastName = "lName",
                Email = $"email{Settings.RandomInt()}@test.com",
                Password = "password",
                Key = Settings.RandomString()
            };
            var signUpCustomerCommand = new SignUpCustomerCommand(customerDraft);
            var signInResult = (CustomerSignInResult) await _client.ExecuteAsync(signUpCustomerCommand);
            var customer = signInResult.Customer;
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key}");
        }


        private async Task ExecuteByBuilder()
        {
            //Create Store
            var store = await _client.Builder().Stores().Create(
                new StoreDraft
                {
                    Key = $"store_{Settings.RandomInt()}",
                }).ExecuteAsync();

            var signInResult = (CustomerSignInResult) await _client
                .Builder()
                .Customers()
                .SignUp(
                    new CustomerDraft
                    {
                        FirstName = "fName",
                        LastName = "lName",
                        Email = $"email{Settings.RandomInt()}@test.com",
                        Password = "password",
                        Key = Settings.RandomString()
                    })
                .InStore(store.Key)
                .ExecuteAsync();

            var customer = signInResult.Customer;
            var customerStore = customer.Stores.FirstOrDefault();
            Console.WriteLine($"Customer Created with Id : {customer.Id} and Key : {customer.Key} in store: {customerStore?.Key}");
        }


        /// <summary>
        /// Get Customer Draft with Custom Fields
        /// </summary>
        /// <returns></returns>
        private CustomerDraft GetCustomerDraftWithCustomFields()
        {
            return new CustomerDraft
            {
                FirstName = "fName",
                LastName = "lName",
                Email = $"email{Settings.RandomInt()}@test.com",
                Password = "password",
                Key = Settings.RandomString(),
                Custom = GetCustomFieldsDraft()
            };
        }

        /// <summary>
        /// Get Custom Fields Draft
        /// </summary>
        /// <returns></returns>
        private CustomFieldsDraft GetCustomFieldsDraft()
        {
            var customFieldsDraft = new CustomFieldsDraft()
            {
                Type = new ResourceIdentifier<Type>()
                {
                    Key = "shoe-size-key"
                },
                Fields = GetCustomFields()
            };
            return customFieldsDraft;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        private Fields GetCustomFields()
        {
            Fields fields = new Fields();
            fields.Add("shoe-size-field", 42); //set the shoeSizeField
            return fields;
        }
    }
}