using System.Threading.Tasks;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public interface IDataTransfer<T>
        where T : new()
    {
        Task SaveDataAsync(T data);
    }
}