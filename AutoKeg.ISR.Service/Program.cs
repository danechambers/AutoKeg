using System;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;

namespace AutoKeg.ISR.Service
{
    class Program
    {
        static PulseCounter Counter { get; } = PulseCounter.Instance;
        static IDataTransfer<PulseDTO> DataTransfer { get; } =
            new MongoDataTransfer<PulseDTO>("someurl", "somedb", "somecollection");

        static void Main(string[] args)
        {
            Console.WriteLine("Gpio Interrupts");
            var pin = 4;

            using (var pinListener = new GpioPinListener(pin))
            using (var snapshotCounter = new SnapshotCount(60000, Counter))
            {
                Console.WriteLine($"Listening on pin {pin}");

                pinListener.RegisterISRCallback(() => Counter.CurrentCount++);

                // subscribe to counter pulse snapshot event
                snapshotCounter.PulseSnapshot += async (s, e) =>
                    await DataTransfer.SaveData(e.PulseData);

                Console.ReadKey();
            }

            Console.WriteLine("Goodbye...");
        }
    }
}
