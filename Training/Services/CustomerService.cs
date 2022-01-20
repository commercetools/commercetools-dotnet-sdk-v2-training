using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.CustomerGroups;
using commercetools.Api.Models.Customers;
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

        public async Task<ICustomer> GetCustomerByKey(string customerKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .WithKey(customerKey)
                .Get()
                .ExecuteAsync();
        }

        public async Task<ICustomer> CreateCustomer(ICustomerDraft customerDraft)
        {
            var customerSignInResult =  await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Customers()
                .Post(customerDraft)
                .ExecuteAsync();
            return customerSignInResult.Customer;
        }

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

        public async Task<ICustomerGroup> GetCustomerGroupByKey(string customerGroupKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .CustomerGroups()
                .WithKey(customerGroupKey)
                .Get()
                .ExecuteAsync();
        }

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