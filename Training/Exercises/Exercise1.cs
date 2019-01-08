using System;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;

namespace Training
{
    public class Exercise1 : IExercise
    {
        private readonly IClient _commercetoolsClient;

        // by using services.UseCommercetools() on the services object in the Program.cs
        // an instance of IClient is registered on the DI container, which we are retrieving here by using constructor injection.
        public Exercise1(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public void Execute()
        {
            var project =  _commercetoolsClient.ExecuteAsync(new GetProjectCommand()).Result;
            Console.WriteLine(project.Name);
        }
    }
}