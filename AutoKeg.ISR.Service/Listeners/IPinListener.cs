using System;

namespace AutoKeg.ISR.Service.Listeners
{
    public interface IPinListener : IDisposable
    {
        int Pin { get; }
        void RegisterISRCallback(Action callBack);
    }
}