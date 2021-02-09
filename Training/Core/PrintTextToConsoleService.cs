using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Training
{
    public class PrintTextToConsoleService : IHostedService
    {
        private readonly IExercise _exercise;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public PrintTextToConsoleService(IExercise exercise, IHostApplicationLifetime lifetime)
        {
            this._exercise = exercise;
            this._applicationLifetime = lifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _exercise.ExecuteAsync().FireAndForgetSafeAsync();
            _applicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }

}
