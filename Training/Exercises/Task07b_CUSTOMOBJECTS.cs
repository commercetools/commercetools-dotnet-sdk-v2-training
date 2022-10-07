using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
using System.Text.Json;
using System.IO;
using commercetools.Sdk.Api.Extensions;
using commercetools.Sdk.Api.Models.CustomObjects;

namespace Training
{
    /// <summary>
    /// Create a Custom Object Exercise
    /// </summary>
    public class Task07B : IExercise
    {
        private readonly IClient _client;

        public Task07B(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
        }

        public async Task ExecuteAsync()
        {
            var jsonFile = "Resources/compatibility-info.json";

            // TODO: CREATE a new custom object     
            
            //Console.WriteLine($"custom object created with Id {customObject.Id} with version {customObject.Version}");
        }



        /// <summary>
        /// creates or updates a custom object
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        private async Task<ICustomObject> CreateCustomObject(string container, string key, string jsonFile)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .CustomObjects()
                .Post(
                    new CustomObjectDraft
                    {
                        Container = container,
                        Key = key,
                        Value = JsonSerializer.Deserialize<CompatibilityInfo>(File.ReadAllText(jsonFile))
                    }
                    )
                .ExecuteAsync();
        }
    }

    

public class ExtraInfo
{
    private string Title { get; set; }
    private string Distance { get; set; }
    private string Watering { get; set; }
}

public class CompatibilityInfo
{
    private List<string> IncompatibleSKUs { get; set; }
    private string LeafletID { get; set; }
    private ExtraInfo Instructions { get; set; }
}
}