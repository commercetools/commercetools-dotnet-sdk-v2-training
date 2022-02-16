using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// Create a Custom Object Exercise
    /// </summary>
    public class Task07B : IExercise
    {
        private readonly IClient _client;
         private readonly CustomObjectService _customObjectsService;
        
        public Task07B(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _customObjectsService = new CustomObjectService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            var jsonFile = "Resources/compatibility-info.json";
            
            //Creates a new custom object 
            var customObject = await _customObjectsService.createCustomObject("compatibility-info", "tulip-seed-product",jsonFile);

            Console.WriteLine($"custom object created with Id {customObject.Id} with version {customObject.Version}");
        }
    }
}