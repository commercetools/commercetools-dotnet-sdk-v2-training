using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;

namespace Training
{
    public class Exercise1 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        // by using services.UseCommercetools() on the services object in the Program.cs
        // an instance of IClient is registered on the DI container, which we are retrieving here by using constructor injection.
        public Exercise1(IEnumerable<IClient> clients)
        {
            if (clients == null || !clients.Any())
            {
                throw new ArgumentNullException(nameof(clients));
            }
            this._commercetoolsClient = clients.FirstOrDefault(c => c.Name == Settings.DEFAULTCLIENT);// the default client
        }
        public void Execute()
        {
            var project =  _commercetoolsClient.ExecuteAsync(new GetProjectCommand()).Result;
            Console.WriteLine(project.Name);
        }
    }
}