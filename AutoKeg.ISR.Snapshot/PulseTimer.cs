using System;

namespace AutoKeg.ISR.Snapshot
{
    public class PulseTimer
    {
        private TimeSpan PulseIdleThreshold { get; }
        // public Stopwatch Timer { get; } = Stopwatch.StartNew();

        public event EventHandler ThresholdReached;

        public PulseTimer(TimeSpan idleThreshold)
        {
            PulseIdleThreshold = idleThreshold;
            StartCountdown();
        }

        private void StartCountdown()
        {
            while(true)
            {
            }
        }

        private void OnThresholdReached(EventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
                handler(this, e);
        }
    }
}