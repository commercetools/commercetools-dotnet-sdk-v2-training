using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;

namespace Training
{
    /// <summary>
    /// Create new customer (SignUp customer)
    /// </summary>
    public class Exercise2 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        private static Random random = new Random();
        
        public Exercise2(IClient commercetoolsClient)
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
                Console.WriteLine(customer.Id);
                Console.WriteLine(customer.Version);
            }
        }


        private CustomerDraft GetCustomerDraft()
        {
            return new CustomerDraft
            {
                FirstName = "userName",
                LastName =  "test",
                Email = $"siaw{random.Next()}@asdf.com",
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
                FirstName = "userName",
                LastName =  "test",
                Email = $"siaw{random.Next()}@asdf.com",
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
                    Key = "Shoe-Size-Key"
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
            fields.Add("Shoe-Size-field", 42);//set the shoeSizeField
            return fields;
        }
        
    }
}