using System;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.GraphQL;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Exercise25 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public Exercise25(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            string graphQuery = "query {customers{count,results{email}}}";
            var graphQlCommand = new GraphQLCommand<GraphQLResult>(new GraphQLParameters(graphQuery));
            //var graphQlCommand = new GraphQLCommand<dynamic>(new GraphQLParameters(graphQuery));
            var result = await _commercetoolsClient.ExecuteAsync(graphQlCommand);
            Console.WriteLine($"Customers count: {result.Data.Customers.Count}");
            Console.WriteLine("Showing Customers emails:");
            foreach (var customer in result.Data.Customers.Results)
            {
                Console.WriteLine(customer.Email);
            }
        }


    }
}
