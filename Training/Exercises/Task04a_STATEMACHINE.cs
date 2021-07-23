using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.States;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training
{
    /// <summary>
    /// create 2 states, stateOrderPacked and stateOrderShipped with Transition
    /// </summary>
    public class Task04A : IExercise
    {
        private readonly IClient _client;

        public Task04A(IClient client)
        {
            this._client = client;
        }

        public async Task ExecuteAsync()
        {
            //create OrderPacked stateDraft, state
            var stateOrderPackedDraft = new StateDraft
            {
                Key = "OrderPacked",
                Initial = true,
                Name = new LocalizedString {{"en", "Order Packed"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderPacked = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .Post(stateOrderPackedDraft)
                .ExecuteAsync();
            
            //create OrderShipped stateDraft, state
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "OrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "Order Shipped"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderShipped = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .Post(stateOrderShippedDraft)
                .ExecuteAsync();
            
            //update packedState to transit to stateShipped
            var update = new StateUpdate()
            {
                Version = stateOrderPacked.Version,
                Actions = new List<IStateUpdateAction>
                {
                    new StateSetTransitionsAction
                    {
                        Transitions = new List<IStateResourceIdentifier>
                        {
                            new StateResourceIdentifier{ Key = stateOrderShipped.Key }
                        }
                    }
                }
            };

            var updatedStateOrderPacked = await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .WithId(stateOrderPacked.Id)
                .Post(update)
                .ExecuteAsync();

            Console.WriteLine($"stateOrderShipped Id : {stateOrderShipped.Id}, stateOrderPacked transition to:  {updatedStateOrderPacked.Transitions[0].Id}");
        }
    }
}