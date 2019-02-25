using System;

namespace AutoKeg.Configuration
{
    public interface IDurationProvider
    {
        TimeSpan Duration { get; }
    }
}