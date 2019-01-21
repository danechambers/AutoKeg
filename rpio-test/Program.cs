using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpio_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gpio Interrupts");
            var pin = Pi.Gpio.Pin04;
            Console.WriteLine($"Listening on pin {pin.PinNumber}");
            pin.PinMode = GpioPinDriveMode.Input;
            pin.RegisterInterruptCallback(EdgeDetection.FallingEdge, ISRCallback);
            Console.ReadKey();
        }

        static void ISRCallback()
        {
            Console.WriteLine("Pin Activated...");
        }
    }
}
