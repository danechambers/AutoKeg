using System.Collections.Generic;
using System.Net;
using System.Text;
using FluentFTP;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutoKeg.Tests.Ftp
{
    public class FtpFixture
    {
        private static readonly List<string> _testData = new List<string>
        {
            "test1", "test2"
        };

        private static readonly string _testFile = "csharp_test1.txt";

        [Test]
        public void TestFtp()
        {
            var jsonData = JsonConvert.SerializeObject(_testData);

            // Get the object used to communicate with the server.
            var ftpClient = new FtpClient();
            ftpClient.Credentials = new NetworkCredential();

            // Copy the contents of the file to the request stream.
            var fileContents = Encoding.UTF8.GetBytes(jsonData);

            var result = ftpClient.Upload(fileContents, $"/{_testFile}");

            Assert.That(result, Is.True);
        }
    }
}