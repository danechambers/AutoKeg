using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public interface IDataTransfer<T> : IDisposable
        where T : new()
    {
        Task SaveDataAsync(T data, CancellationToken cancellationToken = default);
    }
}