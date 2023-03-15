using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using commercetools.Sdk.Api;
using commercetools.Base.Client;
using commercetools.Base.Client.Tokens;
using commercetools.Sdk.ImportApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Training
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<IHostedService, PrintTextToConsoleService>();
                    //start training setup commercetools clients with correct configuration
                    ConfigureServices(services, configuration);
                    // Inject Exercise as Service
                    ConfigureExerciseService(services, args);
                });
            await builder.RunConsoleAsync();
        }

        /// <summary>
        /// Configure commercetools Services and setup clients
        /// </summary>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
             services.UseCommercetoolsApi(configuration, "Client");
            // services.UseCommercetoolsImportApi(configuration, "ImportApiClient");
            // services.UseCommercetoolsApi(configuration, new List<string>{"Client", "BerlinStoreClient"}, CreateDefaultTokenProvider);
            
            var clientConfiguration = configuration.GetSection("Client").Get<ClientConfiguration>();
            Settings.SetCurrentProjectKey(clientConfiguration.ProjectKey);
            
            //For Me endpoint exercise
            //services.AddSingleton(configuration);
        }
        
        public static ITokenProvider CreateDefaultTokenProvider(string clientName, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var clientConfiguration = configuration.GetSection(clientName).Get<ClientConfiguration>();
            return TokenProviderFactory.CreateClientCredentialsTokenProvider(clientConfiguration, httpClientFactory);
        }
        private static void ConfigureExerciseService(IServiceCollection services, string[] args)
        {
            var runningEx = args != null && args.Length > 0 ? args[0] : "02A"; //Task02A is the default exercise
            Type exerciseType = Type.GetType($"Training.Task{runningEx}");
            if (exerciseType != null)
            {
                services.AddSingleton(typeof(IExercise), exerciseType);
            }
            else
            {
                services.AddSingleton<IExercise, DummyExercise>(); // Dummy Ex to show message
            }
        }
        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().
                AddJsonFile("appsettings.test.json").
                AddEnvironmentVariables().
                Build();
        }
    }
}
