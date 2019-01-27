using System;

namespace AutoKeg.ISR.Service.Listeners
{
    public interface IPinListener
    {
        void RegisterISRCallback(Action callBack);
    }
}