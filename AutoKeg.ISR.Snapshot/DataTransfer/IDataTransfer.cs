using System.Threading.Tasks;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public interface IDataTransfer
    {
        void SaveData(object data);
    }

    public interface IDataTransfer<T> : IDataTransfer
        where T : new()
    {
        Task SaveData(T data);
    }
}