using System;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoKeg.ISR.Service
{
    public class Application : BackgroundService
    {
        private IPinListener PinListener { get; }
        private ISnapshotPulse Snapshot { get; }
        private PulseCounter Counter { get; }

        /// <summary>
        /// MS docs suggest to pass service container directly
        /// for consuming scoped services in backgroun tasks
        /// </summary>
        private IServiceProvider Services { get; }

        private ILogger Logger { get; }

        public Application(IPinListener listener,
            ISnapshotPulse snapshot,
            PulseCounter counter,
            IServiceProvider services,
            ILoggerFactory loggerFactory)
        {
            PinListener = listener;
            Snapshot = snapshot;
            Counter = counter;
            Services = services;
            Logger = loggerFactory.CreateLogger<Application>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation($"Listening on pin {PinListener.Pin}");

            PinListener.RegisterISRCallback(() => Counter.CurrentCount++);

            // subscribe to counter pulse snapshot event
            Snapshot.PulseSnapshot += async (s, e) =>
            {
                using (var scope = Services.CreateScope())
                using (var pulseTransfer = scope.ServiceProvider
                    .GetRequiredService<IDataTransfer<PulseDTO>>())
                {
                    await pulseTransfer.SaveDataAsync(e.PulseData, stoppingToken);
                }
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Cancellation Received. Terminating app.");
            PinListener.Dispose();
            Snapshot.Dispose();

            return Task.CompletedTask;
        }
    }
}