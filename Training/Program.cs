using System;
using System.Threading.Tasks;
using commercetools.Sdk.DependencyInjection;
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
                    //start training
                    // setup commercetools client with correct configuration values:
                    services.UseCommercetools(configuration, "Client");
                    // Exercise Start
                    services.AddSingleton<IExercise, Exercise1>();
                    services.AddSingleton<IExercise, Exercise2>();
                });

            await builder.RunConsoleAsync();
            
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