using System;
using AutoKeg.ISR.Snapshot.Events;

namespace AutoKeg.ISR.Snapshot
{
    public interface ISnapshotPulse : IDisposable
    {
        event EventHandler<PulseSnapshotArgs> PulseSnapshot;
    }
}