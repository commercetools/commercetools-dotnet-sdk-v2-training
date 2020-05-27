using System.Linq;
using System.Threading;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;

namespace Training.Extensions
{
    public static class CustomerExtensions
    {
        public static Address GetDefaultShippingAddress(this Customer customer)
        {
            return customer.Addresses.FirstOrDefault(
                a => a.Id != null && a.Id.Equals(customer.DefaultShippingAddressId));
        }
    }
}