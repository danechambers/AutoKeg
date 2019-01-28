using System;
using System.ComponentModel;
using System.Timers;
using AutoKeg.ISR.Snapshot.DataTransfer;

namespace AutoKeg.ISR.Snapshot
{
    public class SnapshotCount : IDisposable
    {
        private PulseCounter Counter { get; }

        private Timer PulseTimer { get; set; }  // race conditions??
        private double WaitForSnapshotInterval { get; } // in milliseconds

        public SnapshotCount(double idleTimer, PulseCounter counter )
        {
            Counter = counter;
            Counter.PropertyChanged += CounterIncremented;
            WaitForSnapshotInterval = idleTimer;
            SetPulseTimer();
        }

        private void SetPulseTimer()
        {
            PulseTimer = new Timer(WaitForSnapshotInterval);
            PulseTimer.Elapsed += OnTimedEvent;
            PulseTimer.AutoReset = true;
            PulseTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Counter.CurrentCount > 0)
            {
                Console.WriteLine($"Sending {Counter.CurrentCount} pulses to db...");
                Counter.CurrentCount = 0;
            }
        }

        private void CounterIncremented(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCount")
            {
                // Reset timer
                PulseTimer.Dispose();
                SetPulseTimer();
            }
        }

        public void Dispose() => PulseTimer.Dispose();
    }
}
