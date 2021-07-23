using System;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Extensions;
using commercetools.Api.Models.CustomObjects;
using commercetools.Base.Client;

namespace Training
{
    /// <summary>
    /// Create a Custom Object Exercise
    /// </summary>
    public class Task07B : IExercise
    {
        private readonly IClient _client;
        
        public Task07B(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            //Create CustomObjectDraft
            var draft = new CustomObjectDraft
            {
                Container = "compatibility-info",
                Key = "tulip6736",
                Value = new
                {
                    IncompatibleSKUs = "tulip6125",
                    LeafletID = "leaflet_1234",
                    Instructions = new
                    {
                        Title = "Flower Handling",
                        Distance = "2 meter",
                        Watering = "heavy"
                    }
                }
            };
            
            //Creates a new custom object 
            

            //Console.WriteLine($"custom object created with Id {customObject.Id} with version {customObject.Version}");
        }
    }
}