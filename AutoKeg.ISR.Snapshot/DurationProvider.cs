using System;
using AutoKeg.ISR.Snapshot.Configuration;
using Microsoft.Extensions.Options;

namespace AutoKeg.ISR.Snapshot
{
    public class DurationProvider : IDurationProvider
    {
        public DurationProvider(IOptions<SnapshotSettings> config)
        {
            var durationSeconds = config.Value.IdleTimer;
            Duration = TimeSpan.FromSeconds(durationSeconds);
        }

        public TimeSpan Duration { get; }
    }
}