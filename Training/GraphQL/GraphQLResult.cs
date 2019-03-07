using System.Collections.Generic;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Customers;

namespace Training.GraphQL
{
    [Endpoint("graphql")]
    public class GraphQLResult
    {
        public GraphResultData Data { get; set; }
    }
    
    public class GraphResultData
    {
        public GraphCustomersResult Customers { get; set; }
    }
    public class GraphCustomersResult
    {
        public int Count { get; set; }
        public List<Customer> Results { get; set; }
    }
}