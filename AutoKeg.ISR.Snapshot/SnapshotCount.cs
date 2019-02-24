using System;
using System.ComponentModel;
using System.Timers;
using AutoKeg.ISR.Snapshot.Events;
using AutoKeg.ISR.Snapshot.DataTransfer;

namespace AutoKeg.ISR.Snapshot
{
    public class SnapshotCount : ISnapshotPulse
    {
        private PulseCounter Counter { get; }

        private Timer PulseTimer { get; set; }  // race conditions??
        private TimeSpan WaitForSnapshotInterval { get; }

        public SnapshotCount(TimeSpan idleTimer, PulseCounter counter)
        {
            Counter = counter;
            Counter.PropertyChanged += CounterIncremented;
            WaitForSnapshotInterval = idleTimer;
            SetPulseTimer();
        }

        private void SetPulseTimer()
        {
            PulseTimer = new Timer(WaitForSnapshotInterval.TotalMilliseconds);
            PulseTimer.Elapsed += OnTimedEvent;
            PulseTimer.AutoReset = true;
            PulseTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Counter.CurrentCount > 0)
            {
                Console.WriteLine($"Sending {Counter.CurrentCount} pulses to db...");
                SnapshotPulseCount(Counter.CurrentCount);
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

        #region snapshot event

        public event EventHandler<PulseSnapshotArgs> PulseSnapshot;

        private void SnapshotPulseCount(int pulseCount)
        {
            var handler = PulseSnapshot;
            if (handler == null)
                return;

            var dto = new PulseDTO { Count = pulseCount };
            handler(this, new PulseSnapshotArgs(dto));
        }

        #endregion
    }
}
