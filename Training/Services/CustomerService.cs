using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.CustomerGroups;
using commercetools.Sdk.Api.Models.Customers;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class CustomerService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public CustomerService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }
        /// <summary>
        /// GET Customer by key
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<ICustomer> GetCustomerByKey(string customerKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .WithKey(customerKey)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// POST a Customer sign-up
        /// </summary>
        /// <param name="customerDraft"></param>
        /// <returns></returns>
        public async Task<ICustomer> CreateCustomer(ICustomerDraft customerDraft)
        {
            var customerSignInResult =  await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();
            return customerSignInResult.Customer;
        }

        /// <summary>
        /// Create a Customer Token
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<ICustomerToken> CreateCustomerToken(ICustomer customer)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .EmailToken()
                .Post(
                    new CustomerCreateEmailToken{
                        Id = customer.Id,
                        Version = customer.Version,
                        TtlMinutes = 10
                    }
                )
                .ExecuteAsync();
        }

        /// <summary>
        /// Confirm a Customer Email
        /// </summary>
        /// <param name="customerToken"></param>
        /// <returns></returns>
        public async Task<ICustomer> ConfirmCustomerEmail(ICustomerToken customerToken)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .EmailConfirm()
                .Post(
                    new CustomerEmailVerify{
                        TokenValue = customerToken.Value
                    }
                )
                .ExecuteAsync();
        }

        /// <summary>
        /// GET Customer Group by key
        /// </summary>
        /// <param name="customerGroupKey"></param>
        /// <returns></returns>
        public async Task<ICustomerGroup> GetCustomerGroupByKey(string customerGroupKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .CustomerGroups()
                .WithKey(customerGroupKey)
                .Get()
                .ExecuteAsync();
        }

        /// <summary>
        /// POST Set Customer Group update for the customer
        /// </summary>
        /// <param name="customerKey"></param>
        /// <param name="customerGroupKey"></param>
        /// <returns></returns>
        public async Task<ICustomer> AssignCustomerToCustomerGroup(string customerKey, string customerGroupKey)
        {
            ICustomer customer = await GetCustomerByKey(customerKey);

            var customerUpdate = new CustomerUpdate
            {
                Version = customer.Version,
                Actions = new List<ICustomerUpdateAction>
                {
                    new CustomerSetCustomerGroupAction
                    {
                        CustomerGroup = new CustomerGroupResourceIdentifier
                        {
                            Key = customerGroupKey
                        }
                    }
                }
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .Customers()
                    .WithKey(customer.Key)
                    .Post(customerUpdate)
                    .ExecuteAsync();
        }
        
    }
}