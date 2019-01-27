using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace AutoKeg.ISR.Snapshot
{
    public class SnapshotCount
    {
        private static PulseCounter Counter { get; } = PulseCounter.Instance;

        private Timer PulseTimer { get; }
        private TimeSpan WaitForSnapshotInterval { get; }

        public SnapshotCount(TimeSpan sendCountInterval)
        {
            Counter.PropertyChanged += CounterIncremented;
            WaitForSnapshotInterval = sendCountInterval;

            PulseTimer = new Timer(sendCountInterval.Milliseconds);
            PulseTimer.Elapsed += OnTimedEvent;
            PulseTimer.AutoReset = true;
            PulseTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Counter.CurrentCount > 0)
                Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }

        private void CounterIncremented(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCount")
            {
                // PulseTimer.;
            }
        }
    }
}
