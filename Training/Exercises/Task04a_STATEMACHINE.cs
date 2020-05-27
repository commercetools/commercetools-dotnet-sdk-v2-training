using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Common;
using commercetools.Sdk.Domain.States;
using commercetools.Sdk.Domain.States.UpdateActions;
using commercetools.Sdk.HttpApi.CommandBuilders;

namespace Training
{
    /// <summary>
    /// create 2 states, stateOrderPacked and stateOrderShipped with Transition
    /// </summary>
    public class Task04A : IExercise
    {
        private readonly IClient _client;

        public Task04A(IClient commercetoolsClient)
        {
            this._client =
                commercetoolsClient ?? throw new ArgumentNullException(nameof(commercetoolsClient));
        }

        public async Task ExecuteAsync()
        {
            //await ExecuteByCommands();
            await ExecuteByBuilder();
        }

        private async Task ExecuteByCommands()
        {
            var stateOrderPackedDraft = new StateDraft
            {
                Key = "OrderPacked",
                Initial = true,
                Name = new LocalizedString {{"en", "Order Packed"}},
                Type = StateType.OrderState
            };
            var stateOrderPacked = await _client.ExecuteAsync(new CreateCommand<State>(stateOrderPackedDraft));
            
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "OrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "Order Shipped"}},
                Type = StateType.OrderState
            };
            var stateOrderShipped = await _client.ExecuteAsync(new CreateCommand<State>(stateOrderShippedDraft));
            
            var action = new SetTransitionsUpdateAction
            {
                Transitions = new List<IReference<State>>
                {
                    stateOrderShipped.ToKeyResourceIdentifier()
                }
            };

            var updatedStateOrderPacked = await _client
                .ExecuteAsync(stateOrderPacked.UpdateByKey(
                    actions => actions.AddUpdate(action)));
            
            Console.WriteLine($"stateOrderShipped Id : {stateOrderShipped.Id}, stateOrderPacked transition to:  {updatedStateOrderPacked.Transitions[0].Id}");
        }

        private async Task ExecuteByBuilder()
        {
            var stateOrderPackedDraft = new StateDraft
            {
                Key = "OrderPacked",
                Initial = true,
                Name = new LocalizedString {{"en", "Order Packed"}},
                Type = StateType.OrderState
            };
            var stateOrderPacked = await _client
                .Builder()
                .States()
                .Create(stateOrderPackedDraft)
                .ExecuteAsync();
            
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "OrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "Order Shipped"}},
                Type = StateType.OrderState
            };
            var stateOrderShipped = await _client
                .Builder()
                .States()
                .Create(stateOrderShippedDraft)
                .ExecuteAsync();
            
            var action = new SetTransitionsUpdateAction
            {
                Transitions = new List<IReference<State>>
                {
                    stateOrderShipped.ToKeyResourceIdentifier()
                }
            };

            var updatedStateOrderPacked = await _client
                .ExecuteAsync(stateOrderPacked.UpdateByKey(
                    actions => actions.AddUpdate(action)));
            
            Console.WriteLine($"stateOrderShipped Id : {stateOrderShipped.Id}, stateOrderPacked transition to:  {updatedStateOrderPacked.Transitions[0].Id}");
        }
    }
}