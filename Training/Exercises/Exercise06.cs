using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;

namespace Training
{
    public class Exercise06 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        // by using services.UseCommercetools() on the services object in the Program.cs
        // an instance of IClient is registered on the DI container, which we are retrieving here by using constructor injection.
        public Exercise06(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._commercetoolsClient = clients.FirstOrDefault(c => c.Name == Settings.DEFAULTCLIENT);// the default client
        }
        public async Task ExecuteAsync()
        {
            var project =  await _commercetoolsClient.ExecuteAsync(new GetProjectCommand());
            Console.WriteLine(project.Name);
        }
    }
}
