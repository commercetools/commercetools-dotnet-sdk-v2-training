using System;
using System.Text.Json;
using System.Threading.Tasks;
using commercetools.Api.Client;
using commercetools.Api.Models.GraphQl;
using commercetools.Base.Client;
using commercetools.Base.Serialization;
using Training.GraphQL;

namespace Training
{
    /// <summary>
    /// GraphQl Query Exercise
    /// </summary>
    public class Task06C : IExercise
    {
        private readonly IClient _client;
        
        public Task06C(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            var graphRequest = new GraphQLRequest
            {
                Query = "query {customers{count,results{email}}}"
            };
            var response = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .Graphql()
                .Post(graphRequest)
                .ExecuteAsync();

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