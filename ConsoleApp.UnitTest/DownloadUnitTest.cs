using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace ConsoleApp.UnitTest
{
    [TestFixture]
    public class DownloadUnitTest
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;

        public DownloadUnitTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _uri = config["Download_URI"];
            _httpClient = new HttpClient { BaseAddress = new Uri(_uri) };
        }

        [Test]
        public void Test_Download_Sync()
        {
            Debug.WriteLine(Environment.CurrentManagedThreadId.ToString());

            var response = _httpClient.GetAsync(_uri);
            var stream = response.Result.Content.ReadAsStream();
            using var sr = new StreamReader(stream);
            var page = sr.ReadToEnd();
        }
    }
}
