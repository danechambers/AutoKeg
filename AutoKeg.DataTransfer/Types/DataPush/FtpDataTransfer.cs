using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.Configuration;
using AutoKeg.DataTransfer.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AutoKeg.DataTransfer.Types.DataPush
{
    public class FtpDataTransfer : IApi
    {
        private string Url { get; }

        public FtpDataTransfer(IOptions<FtpSettings> settings)
        {
            Url = settings.Value.Url;
        }

        public async Task<IApiResult> PostDataAsync(object data,
            CancellationToken cancellationToken = default)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            using (var client = new WebClient())
            {
                var uploadResult = await client.UploadStringTaskAsync(Url, jsonData);
                return new FtpDataTransferResult(uploadResult);
            }
        }
    }
}