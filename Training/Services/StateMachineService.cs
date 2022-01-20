using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Api.Models.CustomerGroups;
using commercetools.Api.Models.Customers;
using commercetools.Api.Models.States;
using commercetools.Base.Client;
using commercetools.Sdk.Api.Extensions;

namespace Training.Services
{
    public class StateMachineService
    {
        private readonly IClient _client;
        private readonly string _projectKey;
        
        public StateMachineService(IClient client, string projectKey)
        {
            _client = client;
            _projectKey = projectKey;
        }

        public async Task<IState> CreateState(IStateDraft stateDraft)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .Post(stateDraft)
                .ExecuteAsync();
        }

        public async Task<IState> GetStateByKey(string stateKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .WithKey(stateKey)
                .Get()
                .ExecuteAsync();
        }

        public async Task<IState> AddTransition(string stateKey, List<string> transitionStateKeys)
        {
            IState state = await GetStateByKey(stateKey);
            var transitionStateIdentifiers = new List<IStateResourceIdentifier>(); 
            foreach (var transitionStateKey in transitionStateKeys)
            {
                transitionStateIdentifiers.Add(new StateResourceIdentifier{ Key = transitionStateKey } );
            };
            var stateUpdate = new StateUpdate
            {
                Version = state.Version,
                Actions = new List<IStateUpdateAction>
                {
                    new StateSetTransitionsAction
                    {
                        Transitions = transitionStateIdentifiers
                    }
                }
            };
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                    .States()
                    .WithKey(state.Key)
                    .Post(stateUpdate)
                    .ExecuteAsync();
        }
        
    }
}