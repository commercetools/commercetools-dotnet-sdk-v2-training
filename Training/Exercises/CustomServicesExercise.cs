using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using Training.CustomServices.Domain.Stores;
using Training.CustomServices.Domain.Stores.UpdateActions;

namespace Training
{
    public class CustomServicesExercise : IExercise
    {
        private readonly IClient _commercetoolsClient;

        public CustomServicesExercise(IClient commercetoolsClient)
        {
            this._commercetoolsClient =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }
        public async Task ExecuteAsync()
        {
            //Create Store
            var storeDraft = this.GetStoreDraft();
            var store = await _commercetoolsClient.ExecuteAsync(new CreateCommand<Store>(storeDraft));
            Console.WriteLine($"New Store Created with Id {store.Id}");
            await GetStoreById(store.Id);
            await GetStoreByKey(store.Key);
            await QueryAllStores();
            await UpdateStoreNameByKey(store.Key);
            await DeleteStoreByKey(store.Key);
        }

        private async Task GetStoreById(string id)
        {
            var store = await _commercetoolsClient.ExecuteAsync(new GetByIdCommand<Store>(id));
            Console.WriteLine($"Get Store By Id, Store name: {store.Name["en"]}");
        }
        private async Task GetStoreByKey(string key)
        {
            var store = await _commercetoolsClient.ExecuteAsync(new GetByKeyCommand<Store>(key));
            Console.WriteLine($"Get Store By Key, Store name: {store.Name["en"]}");
        }

        private async Task QueryAllStores()
        {
            var queryCommand = new QueryCommand<Store>();
            var returnedSet = await _commercetoolsClient.ExecuteAsync(queryCommand);
            if (returnedSet.Results.Count > 0)
            {
                Console.WriteLine("Stores: ");
                foreach (var store in returnedSet.Results)
                {
                    Console.WriteLine(store.Name["en"]);
                }
            }
        }

        private async Task UpdateStoreNameByKey(string key)
        {
            var retrievedStore = await _commercetoolsClient
                .ExecuteAsync(new GetByKeyCommand<Store>(key));

            var setNameUpdateAction = new SetNameUpdateAction
            {
                Name = new LocalizedString {{"en", $"Store_{Settings.RandomInt()}"}}
            };
            var updateActions = new List<UpdateAction<Store>> { setNameUpdateAction };

            var updatedStore = await _commercetoolsClient
                .ExecuteAsync(new UpdateByKeyCommand<Store>(key,retrievedStore.Version, updateActions));

            Console.WriteLine($"Store with name {retrievedStore.Name["en"]} has been updated with name {updatedStore.Name["en"]}");
        }

        private async Task DeleteStoreByKey(string key)
        {
            var retrievedStore = await _commercetoolsClient
                .ExecuteAsync(new GetByKeyCommand<Store>(key));

            var deletedStore = await _commercetoolsClient
                .ExecuteAsync(new DeleteByKeyCommand<Store>(retrievedStore.Key, retrievedStore.Version));

            Console.WriteLine($"Store {retrievedStore.Name["en"]} has been deleted");
        }
        private StoreDraft GetStoreDraft()
        {
            var randInt = Settings.RandomInt();
            var storeDraft = new StoreDraft
            {
                Name = new LocalizedString {{"en", $"Store_{randInt}"}},
                Key = $"Key_{randInt}"
            };
            return storeDraft;
        }
    }
}
