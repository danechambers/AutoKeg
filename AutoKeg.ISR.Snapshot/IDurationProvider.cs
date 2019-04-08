using System;

namespace AutoKeg.ISR.Snapshot
{
    public interface IDurationProvider
    {
        TimeSpan Duration { get; }
    }
}