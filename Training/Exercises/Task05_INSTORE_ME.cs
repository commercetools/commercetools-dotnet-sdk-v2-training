using System.Threading.Tasks;
using commercetools.Base.Client;

namespace Training
{
    public class Task05
    {
        private readonly IClient _client;

        public Task05(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            // TODO: Create in-store cart with global api client
            //  Provide an api client with global permissions
            //  Provide a customer with only store permissions
            //
        }
    }
}