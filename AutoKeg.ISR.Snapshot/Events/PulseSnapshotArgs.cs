using System;
using AutoKeg.DataTransfer.DTOs;

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