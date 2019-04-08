using System.IO;
using System.Net;
using System.Text;
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
            var uploadContents = Encoding.UTF8.GetBytes(jsonData);
            var request = MakeRequest("test_file.txt", uploadContents.Length);

            using (var requestStream = request.GetRequestStream())
            {
                await requestStream.WriteAsync(
                    uploadContents, 0, uploadContents.Length, cancellationToken);
            }

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return new FtpDataTransferResult(response.StatusDescription);
            }
        }

        private FtpWebRequest MakeRequest(string uploadFileName, long contentLength)
        {
            var request = (FtpWebRequest)WebRequest.Create($"{Url}/{uploadFileName}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential();
            request.ContentLength = contentLength;
            return request;
        }
    }
}