using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;

namespace Training
{
    /// <summary>
    /// Create new customer (SignUp customer)
    /// </summary>
    public class Exercise7 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise7(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            var customerDraft = this.GetCustomerDraft();
            var signUpCustomerCommand = new SignUpCustomerCommand(customerDraft);
            var result =  _commercetoolsClient.ExecuteAsync(signUpCustomerCommand).Result as CustomerSignInResult;
            var customer = result?.Customer;
            if (customer != null)
            {
                Console.WriteLine($"Customer Created with Id : {customer.Id}");
            }
        }


        private CustomerDraft GetCustomerDraft()
        {
            return new CustomerDraft
            {
                FirstName = "fName",
                LastName =  "lName",
                Email = $"email{Settings.RandomInt()}@test.com",
                Password = "password"
            };
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
                LastName =  "lName",
                Email = $"email{Settings.RandomInt()}@test.com",
                Password = "password",
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
                Type = new ResourceIdentifier()
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
            fields.Add("shoe-size-field", 42);//set the shoeSizeField
            return fields;
        }

    }
}
