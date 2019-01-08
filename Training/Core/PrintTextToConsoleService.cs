using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using Microsoft.Extensions.Hosting;

namespace Training
{
    public class PrintTextToConsoleService : IHostedService
    {
        private readonly IEnumerable<IExercise> _extercises;

        public PrintTextToConsoleService(IEnumerable<IExercise> exercises)
        {
            this._extercises = exercises;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var exercise in _extercises)
            {
                exercise.Execute();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }

}