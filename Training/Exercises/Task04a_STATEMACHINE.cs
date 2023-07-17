using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.States;
using commercetools.Base.Client;
using Training.Services;

namespace Training
{
    /// <summary>
    /// create 2 states, stateOrderPacked and stateOrderShipped with Transition
    /// </summary>
    public class Task04A : IExercise
    {
        private readonly IClient _client;
        private readonly StateMachineService _stateMachineService;

        public Task04A(IEnumerable<IClient> clients)
        {
            _client = clients.FirstOrDefault(c => c.Name.Equals("Client"));
            _stateMachineService = new StateMachineService(_client, Settings.ProjectKey);
        }

        public async Task ExecuteAsync()
        {
            // CREATE OrderPacked stateDraft, state
            var stateOrderPackedDraft = new StateDraft
            {
                Key = "ndOrderPacked",
                Initial = true,
                Name = new LocalizedString {{"en", "ND Order Packed"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderPacked = await _stateMachineService.CreateState(
                stateOrderPackedDraft);

            // CREATE OrderShipped stateDraft, state
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "ndOrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "ND Order Shipped"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderShipped = await _stateMachineService.CreateState(
                stateOrderShippedDraft);

            // UPDATE packedState to transit to stateShipped
            stateOrderPacked = await _stateMachineService.AddTransition(
                    stateOrderPacked.Key,
                    new List<string> { stateOrderShipped.Key }
                );
            stateOrderShipped = await _stateMachineService.AddTransition(
                    stateOrderShipped.Key,
                    new List<string>()
                );
            Console.WriteLine($"stateOrderShipped Id : {stateOrderShipped.Id}, stateOrderPacked transition to:  {stateOrderPacked.Transitions[0].Id}");
        }
    }
}