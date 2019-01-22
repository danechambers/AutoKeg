﻿using System;
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
            var pin = Pi.Gpio.Pin07;
            Console.WriteLine($"Listening on pin {pin.PinNumber}");
            pin.PinMode = GpioPinDriveMode.Input;
            pin.RegisterInterruptCallback(EdgeDetection.RisingAndFallingEdges, ISRCallback);
            Console.ReadKey();
            Console.WriteLine($"{pulseCount}");
        }

        static void ISRCallback() => pulseCount++;
    }
}
