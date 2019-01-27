using System;
using AutoKeg.ISR.Service.Listeners;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace AutoKeg.ISR.Service
{
    class Program
    {
        static volatile int pulseCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Gpio Interrupts");
            var pin = 7;

            using(var pinListener = new GpioPinListener(pin))
            {
                Console.WriteLine($"Listening on pin {pin}");
                pinListener.RegisterISRCallback(ISRCallback);
                Console.ReadKey();
            }

            Console.WriteLine($"{pulseCount}");
        }

        static void ISRCallback() => pulseCount++;
    }
}
