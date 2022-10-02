using System.Linq;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.Customers;

namespace Training.Extensions
{
    public static class CustomerExtensions
    {
        public static IAddress GetDefaultShippingAddress(this ICustomer customer)
        {
            return customer.Addresses.FirstOrDefault(
                a => a.Id != null && a.Id.Equals(customer.DefaultShippingAddressId));
        }
    }
}