using System;
using System.Collections.Generic;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace AutoKeg.ISR.Service.Listeners
{
    public class GpioPinListener : IPinListener
    {
        private static GpioController Gpio { get; } = Pi.Gpio;
        private GpioPin GpioPin { get; }
        public int Pin { get; }

        public GpioPinListener(int bcmPinNumber)
        {
            if (!(PinMap.TryGetValue(bcmPinNumber, out var pin)))
                throw new ArgumentException($"{bcmPinNumber} is not a valid pin");

            Pin = bcmPinNumber;
            GpioPin = pin;
            GpioPin.PinMode = GpioPinDriveMode.Input;
        }

        public void RegisterISRCallback(Action callBack) =>
            GpioPin.RegisterInterruptCallback(
                EdgeDetection.FallingEdge,
                callBack.Invoke);

        public void Dispose() => Gpio.Dispose();

        private static Dictionary<int, GpioPin> PinMap { get; } =
            new Dictionary<int, GpioPin>
            {
                { 4, Gpio.Pin07 },
            };
    }
}