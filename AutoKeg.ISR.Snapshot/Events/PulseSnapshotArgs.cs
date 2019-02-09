using System;
using AutoKeg.ISR.Snapshot.DataTransfer;

namespace AutoKeg.ISR.Snapshot.Events
{
    public class PulseSnapshotArgs : EventArgs
    {
        public PulseDTO PulseData { get; }
        public PulseSnapshotArgs(PulseDTO pulseData)
        {
            PulseData = pulseData;
        }
    }
}