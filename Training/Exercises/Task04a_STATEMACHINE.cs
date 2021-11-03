using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Extensions;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.States;
using commercetools.Base.Client;

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
            
            
            //create OrderShipped stateDraft, state
            var stateOrderShippedDraft = new StateDraft
            {
                Key = "OrderShipped",
                Initial = false,
                Name = new LocalizedString {{"en", "Order Shipped"}},
                Type = IStateTypeEnum.OrderState
            };
            
            
            //update packedState to transit to stateShipped
            
            //Console.WriteLine($"stateOrderShipped Id : {stateOrderShipped.Id}, stateOrderPacked transition to:  {updatedStateOrderPacked.Transitions[0].Id}");
        }
    }
}