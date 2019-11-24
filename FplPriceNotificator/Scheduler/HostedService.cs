using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace FplPriceNotificator.Scheduler
    {
    public abstract class HostedService : IHostedService
    {
        private Task _executingTask;
        private CancellationTokenSource _cts;

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _executingTask = ExecuteAsync(cancellationToken);
                return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
            }

            public async Task StopAsync(CancellationToken cancellationToken)
            {
                // Stop called without start
                if (_executingTask==null)
                {
                    return;
                }

                // Signal cancellation to the executing method
                _cts.Cancel();

                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

                // Throw if cancellation triggered
                cancellationToken.ThrowIfCancellationRequested();
            }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
    }
