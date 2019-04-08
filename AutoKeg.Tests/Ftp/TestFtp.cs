using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

        [Ignore("requires working ftp server")]
        [Test]
        public void TestFtp()
        {
            var jsonData = JsonConvert.SerializeObject(_testData);
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://192.168.1.199/{_testFile}");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("upload", "testltd123");

            // Copy the contents of the file to the request stream.
            var fileContents = Encoding.UTF8.GetBytes(jsonData);

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }

            // Assert.That(uploadResult, Is.Not.Empty);
        }
    }
}