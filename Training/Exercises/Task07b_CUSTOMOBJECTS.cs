using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Base.Client;
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
            var compatibilityInfo = new CompatibilityInfo
            {
                IncompatibleSKUs = new List<string> { "tulip-seed-product" },
                LeafletID = "leaflet_1234",
                Instructions = new ExtraInfo
                {
                    Title = "Flower Handling",
                    Distance = "2 meter",
                    Watering = "heavy"
                }
            };

            
            // TODO: CREATE a new custom object     
            
            // Console.WriteLine($"custom object created with Id {customObject.Id} with version {customObject.Version}");
        }



        /// <summary>
        /// creates or updates a custom object
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <param name="compatibilityInfo"></param>
        /// <returns></returns>
        public async Task<ICustomObject> CreateCustomObject(string container, string key, CompatibilityInfo compatibilityInfo)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .CustomObjects()
                .Post(
                    new CustomObjectDraft
                    {
                        Container = container,
                        Key = key,
                        Value = compatibilityInfo
                    }
                )
                .ExecuteAsync();
        }
    }
    public class ExtraInfo
    {
        public string Title { get; set; }
        public string Distance { get; set; }
        public string Watering { get; set; }
    }

    public class CompatibilityInfo
    {
        public List<string> IncompatibleSKUs { get; set; }
        public string LeafletID { get; set; }
        public ExtraInfo Instructions { get; set; }
    }

}