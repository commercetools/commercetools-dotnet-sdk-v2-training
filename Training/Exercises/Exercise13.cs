using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Exercise13 : IExercise
    {
        private readonly IClient _commercetoolsClient;
        
        public Exercise13(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._commercetoolsClient = clients.FirstOrDefault(c => c.Name == Settings.DEFAULTCLIENT);
        }
        public void Execute()
        {
            GetCustomersByGraphQl();
        }

        private void GetCustomersByGraphQl()
        {
            string graphQuery = "query {customers{count,results{email}}}";
            GraphQlQuery query = new GraphQlQuery(graphQuery);
            GraphQLResult result = _commercetoolsClient.ExecuteAsync(new CreateCommand<GraphQLResult>(query)).Result;
            Console.WriteLine($"Customers count: {result.Data.Customers.Count}");
            Console.WriteLine("Showing Customers emails:");
            foreach (var customer in result.Data.Customers.Results)
            {
                Console.WriteLine(customer.Email);
            }
        }
        
    }
}