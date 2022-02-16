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
    public class Task05B : IExercise
    {
        private readonly IServiceProvider serviceProvider;

        public Task05B(IServiceProvider serviceProvider)
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
    
            // // TODO: USE meClient to get my Customer Profile
            
            //Console.WriteLine($"My Profile, firstName:{myProfile.FirstName}, lastName:{myProfile.LastName}");
            
            /*
            // // TODO: USE meClient to get my Orders
            
            
            Console.WriteLine($"Orders count: {myOrders.Count}");
            foreach (var order in myOrders.Results)
            {
                Console.WriteLine($"{order.Id}");
            }
            */
            
        }
    }
}