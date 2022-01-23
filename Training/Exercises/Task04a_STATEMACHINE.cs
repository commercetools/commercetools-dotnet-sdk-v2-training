using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.States;
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
            // create OrderPacked stateDraft, state
            var stateOrderPackedDraft = new StateDraft
            {
                Key = "mg-OrderPacked",
                Initial = true,
                Name = new LocalizedString {{"en", "MG Order Packed"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderPacked = await _stateMachineService.CreateState(stateOrderPackedDraft);
            Console.WriteLine($"New state created: {stateOrderPacked.Id}");
            
            // create OrderShipped stateDraft, state
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "mg-OrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "MG Order Shipped"}},
                Type = IStateTypeEnum.OrderState
            };
            var stateOrderShipped = await _stateMachineService.CreateState(stateOrderShippedDraft);
            Console.WriteLine($"New state created: {stateOrderShipped.Id}");
            
            // update statePacked to transit to stateShipped

            var updatedStateOrderPacked = await _stateMachineService.AddTransition(stateOrderPackedDraft.Key,new List<string>{stateOrderShippedDraft.Key});
            Console.WriteLine($"stateOrderPacked Id : {updatedStateOrderPacked.Id}, transition set to:  {updatedStateOrderPacked.Transitions[0].Id}");
            
            // update stateShipped to be the last state
            
            var updatedStateOrderShipped = await _stateMachineService.AddTransition(stateOrderShippedDraft.Key,new List<string>());
            Console.WriteLine($"stateOrderShipped Id : {updatedStateOrderShipped.Id}, transition set to:  {(updatedStateOrderShipped.Transitions.Any()?updatedStateOrderShipped.Transitions[0].Id:"none")}");
        }
    }
}