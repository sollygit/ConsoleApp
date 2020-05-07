using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace ConsoleApp.UnitTest
{
    [TestFixture]
    public class DownloadUnitTest
    {
        string url;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            
            url = config["Download_URI"];
        }
        
        [Test]
        public void Test_Download_Sync()
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            var request = WebRequest.CreateHttp(url);
            // Initial round trip to the server
            var response = request.GetResponse() as HttpWebResponse;
            // Downloading page content
            var stream = response.GetResponseStream();
            using var sr = new StreamReader(stream);
            var page = sr.ReadToEnd();
        }

        [Test]
        // This method would run in the main thread
        public void Test_Download_Async()
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            var request = WebRequest.CreateHttp(url);
            var callback = new AsyncCallback(HttpResponseAvailable);
            var ar = request.BeginGetResponse(callback, request); // Initial round trip to the server

            // Wait for the background thread to complete
            ar.AsyncWaitHandle.WaitOne();
        }

        // This method would run in a background thread
        private static void HttpResponseAvailable(IAsyncResult ar)
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            var request = ar.AsyncState as HttpWebRequest;
            var response = request.EndGetResponse(ar) as HttpWebResponse;
            var stream = response.GetResponseStream(); // Downloading page content
            using var sr = new StreamReader(stream);
            var page = sr.ReadToEnd();
        }
    }
}
