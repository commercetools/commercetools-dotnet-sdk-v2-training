using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return (ExpandoObject) expando;
        }
    }
}