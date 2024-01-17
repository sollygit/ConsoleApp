using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ConsoleApp.UnitTest
{
    [TestFixture]
    public class DatabaseUnitTest
    {
        private readonly string _connectionString;

        public DatabaseUnitTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = config["ConnectionString"];
        }

        [Test]
        public void Test_DB_Sync()
        {
            var query = "SELECT @@VERSION";
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var sqlCommand = new SqlCommand(query, conn);
            using var reader = sqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                var data = reader[0].ToString();
                Debug.WriteLine(data);
            }
        }

        [Test]
        public void Test_DB_Async()
        {
            var query = "SELECT @@VERSION";
            var conn = new SqlConnection(_connectionString);
            
            conn.Open();

            var sqlCommand = new SqlCommand(query, conn);
            var callback = new AsyncCallback(OnDataAvailable);
            var ar = sqlCommand.BeginExecuteReader(callback ,sqlCommand);

            // Wait for the background thread to complete
            ar.AsyncWaitHandle.WaitOne();
        }

        private static void OnDataAvailable(IAsyncResult ar)
        {
            var sqlCommand = ar.AsyncState as SqlCommand;
            using var reader = sqlCommand.EndExecuteReader(ar);

            while (reader.Read())
            {
                var data = reader[0].ToString();
                Debug.WriteLine(data);
            }
        }
    }
}
