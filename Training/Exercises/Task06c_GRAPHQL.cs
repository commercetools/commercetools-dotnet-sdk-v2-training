using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.GraphQl;
using commercetools.Base.Client;
using commercetools.Base.Serialization;
using Training.GraphQL;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Task06C : IExercise
    {
        private readonly IClient _client;

        public Task06C(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
        }

        public async Task ExecuteAsync()
        {
            var graphRequest = new GraphQLRequest
            {
                Query = "query {customers{count,results{email}}}"
            };

            // TODO: graphQL Request
            IGraphQLResponse response = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Graphql()
                .Post(graphRequest)
                .ExecuteAsync();

            //Map Response to the typed result and show it
            var typedResult = ((JsonElement) response.Data).ToObject<GraphResultData>(_client.SerializerService);
            var customersResult = typedResult.Customers;
            Console.WriteLine($"Customers count: {customersResult.Count}");
            Console.WriteLine("Showing Customers emails:");
            foreach (var customer in customersResult.Results)
            {
                Console.WriteLine(customer.Email);
            }
            
        }
    }
}