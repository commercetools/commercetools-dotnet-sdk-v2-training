using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using commercetools.Api.Models.CustomObjects;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class CustomObjectService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public CustomObjectService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        /// <summary>
        /// creates or updates a custom object
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public async Task<ICustomObject> createCustomObject(string container, string key, string jsonFile)
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