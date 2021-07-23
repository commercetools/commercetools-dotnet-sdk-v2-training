using System;
using System.Net.Http;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Serialization;
using commercetools.Base.Client;
using commercetools.Base.Client.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    public class Task08B : IExercise
    {
        private readonly IServiceProvider serviceProvider;

        public Task08B(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync()
        {
            var email = "michele_george@example.com";
            var password = "password";
    
            var configuration = serviceProvider.GetService<IConfiguration>();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var serializerService = serviceProvider.GetService<SerializerService>();
    
            var clientConfiguration = configuration.GetSection("MeClient").Get<ClientConfiguration>();
    
            //Create passwordFlow TokenProvider
            var passwordTokenProvider = TokenProviderFactory
                .CreatePasswordTokenProvider(clientConfiguration,
                    httpClientFactory,
                    new InMemoryUserCredentialsStoreManager(email, password));
    
            //Create MeClient
            var meClient = ClientFactory.Create(
                "MeClient",
                clientConfiguration,
                httpClientFactory,
                serializerService,
                passwordTokenProvider);
    
            //Get Customer Profile
            var myProfile = await meClient
                .WithApi()
                .WithProjectKey(Settings.ProjectKey)
                .Me()
                .Get()
                .ExecuteAsync();
            
            Console.WriteLine($"My Profile, firstName:{myProfile.FirstName}, lastName:{myProfile.LastName}");
            
            /*
            //Get my Orders
            var myOrders = await
                meClient.WithApi()
                    .WithProjectKey(Settings.ProjectKey)
                    .Me()
                    .Orders()
                    .Get()
                    .ExecuteAsync();
            
            Console.WriteLine($"Orders count: {myOrders.Count}");
            foreach (var order in myOrders.Results)
            {
                Console.WriteLine($"{order.Id}");
            }
            */
            
        }
    }
}