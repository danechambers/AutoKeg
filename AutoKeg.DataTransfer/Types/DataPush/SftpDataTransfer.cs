using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.Interfaces;

namespace AutoKeg.DataTransfer.Types.DataPush
{
    public class SftpDataTransfer : IApi
    {
        public Task<IApiResult> PostDataAsync(object data, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}