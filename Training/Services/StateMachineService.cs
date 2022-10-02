using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercetools.Sdk.Api.Models.States;
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

        /// <summary>
        /// GET a state by key
        /// </summary>
        /// <param name="stateKey"></param>
        /// <returns></returns>
        public async Task<IState> GetStateByKey(string stateKey)
        {
            return await _client.WithApi().WithProjectKey(Settings.ProjectKey)
                .States()
                .WithKey(stateKey)
                .Get()
                .ExecuteAsync();
        }


        /// <summary>
        /// Creates a workflow state
        /// </summary>
        /// <param name="stateDraft"></param>
        /// <returns></returns>
        public async Task<IState> CreateState(IStateDraft stateDraft)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST a set transition update for the state
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="transitionStateKeys"></param>
        /// <returns></returns>
        public async Task<IState> AddTransition(string stateKey, List<string> transitionStateKeys)
        {
            throw new NotImplementedException();
        }
        
    }
}