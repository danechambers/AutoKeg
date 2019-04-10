using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.Configuration;
using AutoKeg.DataTransfer.Interfaces;
using FluentFTP;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AutoKeg.DataTransfer.Types.DataPush
{
    // Don't use System.Net.FtpWebRequest from: 
    // https://github.com/dotnet/platform-compat/blob/master/docs/DE0003.md
    public class FtpDataTransfer : IApi
    {
        private string Url { get; }
        private string FolderPath { get; }
        private NetworkCredential FtpCredentials { get; }

        public FtpDataTransfer(IOptions<FtpSettings> settings)
        {
            var ftpSettings = settings.Value;
            Url = ftpSettings.Url;
            FolderPath = ftpSettings.FolderAbsolutePath;
            FtpCredentials = new NetworkCredential(ftpSettings.Username, ftpSettings.Password);
        }

        public async Task<IApiResult> PostDataAsync(object data,
            CancellationToken cancellationToken = default)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var uploadContents = Encoding.UTF8.GetBytes(jsonData);

            using (var client = new FtpClient(Url))
            {
                client.Credentials = FtpCredentials;
                var success = await client.UploadAsync(uploadContents,
                    Path.Combine(FolderPath, $"{DateTime.Now.Ticks}.txt"),
                    token: cancellationToken);
                return new FtpDataTransferResult(success);
            }
        }
    }
}