using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoKeg.ISR.Snapshot
{
    public class PulseCounter : INotifyPropertyChanged
    {
        private static readonly Lazy<PulseCounter> _lazy =
            new Lazy<PulseCounter>(() => new PulseCounter());

        public static PulseCounter Instance { get; } = _lazy.Value;

        private PulseCounter() { }

        private static volatile int _currentCount = 0;

        public int CurrentCount
        {
            get => _currentCount;
            set
            {
                if (value != _currentCount)
                {
                    _currentCount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}