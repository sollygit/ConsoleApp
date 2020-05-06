using NUnit.Framework;
using System;
using System.IO;
using System.Net;

namespace ConsoleApp.UnitTest
{
    [TestFixture]
    public class DownloadUnitTest
    {
        // Mock a 5 sec delay to a page resource
        readonly string url = "http://deelay.me/5000/http://www.delsink.com";

        [Test]
        public void Test_Download()
        {
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
            var request = WebRequest.CreateHttp(url);
            var callback = new AsyncCallback(HttpResponseAvailable);
            var ar = request.BeginGetResponse(callback, request); // Initial round trip to the server

            // await completion before exiting main thread
            ar.AsyncWaitHandle.WaitOne();
        }

        // This method would run in a background thread
        private static void HttpResponseAvailable(IAsyncResult ar)
        {
            var request = ar.AsyncState as HttpWebRequest;
            var response = request.EndGetResponse(ar) as HttpWebResponse;
            var stream = response.GetResponseStream(); // Downloading page content
            using var sr = new StreamReader(stream);
            var page = sr.ReadToEnd();
        }
    }
}
