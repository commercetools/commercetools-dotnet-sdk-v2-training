using System.Linq;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Customers;

namespace Training.Extensions
{
    public static class CustomerExtensions
    {
        public static IAddress GetDefaultShippingAddress(this Customer customer)
        {
            return customer.Addresses.FirstOrDefault(
                a => a.Id != null && a.Id.Equals(customer.DefaultShippingAddressId));
        }
    }
}