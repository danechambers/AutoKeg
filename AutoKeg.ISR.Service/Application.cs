using System;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;

namespace AutoKeg.ISR.Service
{
    public class Application
    {
        private int Pin { get; }
        private IDataTransfer<PulseDTO> PulseTransfer { get; }
        private PulseCounter Counter { get; }

        public Application(int pin,
            PulseCounter counter,
            IDataTransfer<PulseDTO> pulseTransfer)
        {
            Pin = pin;
            PulseTransfer = pulseTransfer;
            Counter = counter;
        }

        public async Task Run(CancellationToken cancelToken) =>
            await Task.Run(() =>
            {
                using (var pinListener = new GpioPinListener(Pin))
                using (var snapshotCounter = new SnapshotCount(60000, Counter))
                {
                    Console.WriteLine($"Listening on pin {Pin}");

                    pinListener.RegisterISRCallback(() => Counter.CurrentCount++);

                    // subscribe to counter pulse snapshot event
                    snapshotCounter.PulseSnapshot += async (s, e) =>
                        await PulseTransfer.SaveDataAsync(e.PulseData);

                    while (true)
                    {
                        if (cancelToken.IsCancellationRequested)
                            cancelToken.ThrowIfCancellationRequested();
                    }
                }
            }, cancelToken);
    }
}