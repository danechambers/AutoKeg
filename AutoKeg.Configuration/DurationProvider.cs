using System;

namespace AutoKeg.Configuration
{
    public class DurationProvider : IDurationProvider
    {
        public DurationProvider(TimeSpan duration)
        {
            Duration = duration;
        }

        public TimeSpan Duration { get; }
    }
}