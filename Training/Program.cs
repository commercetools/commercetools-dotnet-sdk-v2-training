using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using commercetools.Sdk.DependencyInjection;
using commercetools.Sdk.HttpApi.AdditionalParameters;
using commercetools.Sdk.HttpApi.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.MachineLearningExtensions;

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
                    //start training
                    // setup commercetools client with correct configuration values:
                    //services.UseCommercetools(configuration, "Client");
                    ConfigureServices(services, configuration);
                    
                    
                    // Exercise Start
                    //services.AddSingleton<IExercise, Exercise123>(); //Testing calling Machine Learning from .Net HTTP Client
                    services.AddSingleton<IExercise, Exercise9>(); // Excerise to call Machine Learning from .Net SDK
                });

            await builder.RunConsoleAsync();
            
        }

        /// <summary>
        /// Configure commercetools Services and setup clients
        /// </summary>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            IDictionary<string, TokenFlow> clients = new Dictionary<string, TokenFlow>()
            {
                { "MachineLearningClient", TokenFlow.ClientCredentials }, // Machine Learning Client
                { "Client", TokenFlow.ClientCredentials } //default client
            };
            services.AddSingleton<IAdditionalParametersBuilder, GetGeneralCategoriesRecommendationsAdditionalParametersBuilder>();
            services.UseCommercetools(configuration, clients);
            
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