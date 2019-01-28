using System;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace AutoKeg.ISR.Service
{
    class Program
    {
        static PulseCounter Counter { get; } = PulseCounter.Instance;

        static void Main(string[] args)
        {
            Console.WriteLine("Gpio Interrupts");
            var pin = 4;

            using (var pinListener = new GpioPinListener(pin))
            using (var snapshotCounter = new SnapshotCount(60000, Counter))
            {
                Console.WriteLine($"Listening on pin {pin}");
                pinListener.RegisterISRCallback(() => Counter.CurrentCount++);
                Console.ReadKey();
            }

            Console.WriteLine("Goodbye...");
        }
    }
}
