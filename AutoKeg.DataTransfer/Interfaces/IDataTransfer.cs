using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.DataTransfer.Interfaces
{
    public interface IDataTransfer<T> : IDisposable
        where T : new()
    {
        Task SaveDataAsync(T data, CancellationToken cancellationToken = default);
    }
}