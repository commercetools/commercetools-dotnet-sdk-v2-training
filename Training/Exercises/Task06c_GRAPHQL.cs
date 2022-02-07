using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using commercetools.Api.Models.GraphQl;
using commercetools.Base.Client;
using commercetools.Base.Serialization;
using Training.GraphQL;
using Training.Services;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Task06C : IExercise
    {
        private readonly IClient _client;
        private readonly GraphQLService _graphQLService;
        
        public Task06C(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _graphQLService = new GraphQLService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var graphRequest = new GraphQLRequest
            {
                Query = "query {customers{count,results{email}}}"
            };
            var response = await _graphQLService.GetGraphQLQueryResponse(graphRequest);

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