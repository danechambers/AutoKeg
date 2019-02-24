using System;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoKeg.ISR.Service
{
    public class Application : BackgroundService
    {
        private IPinListener PinListener { get; }
        private IDataTransfer<PulseDTO> PulseTransfer { get; }
        private ISnapshotPulse Snapshot { get; }
        private PulseCounter Counter { get; }

        private ILogger Logger { get; }

        public Application(IPinListener listener,
            ISnapshotPulse snapshot,
            PulseCounter counter,
            IDataTransfer<PulseDTO> pulseTransfer,
            ILoggerFactory loggerFactory)
        {
            PinListener = listener;
            PulseTransfer = pulseTransfer;
            Snapshot = snapshot;
            Counter = counter;
            Logger = loggerFactory.CreateLogger<Application>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await Task.Run(() =>
            {
                Logger.LogInformation($"Listening on pin {PinListener.Pin}");

                PinListener.RegisterISRCallback(() => Counter.CurrentCount++);

                // subscribe to counter pulse snapshot event
                Snapshot.PulseSnapshot += async (s, e) =>
                    await PulseTransfer.SaveDataAsync(e.PulseData);

                while (true)
                {
                    if (stoppingToken.IsCancellationRequested)
                    {
                        Logger.LogInformation("Cancellation Received. Terminating app.");
                        stoppingToken.ThrowIfCancellationRequested();
                    }
                }
            }, stoppingToken);
    }
}