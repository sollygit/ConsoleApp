using ConsoleApp.Common;
using ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleApp.Repository
{
    public interface IWarehouseRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<RetailerProduct> RetailerProducts { get; }
    }

    public class WarehouseRepository : IWarehouseRepository
    {
        readonly ILogger _logger;

        public IEnumerable<Product> Products { get; internal set; }
        public IEnumerable<RetailerProduct> RetailerProducts { get; internal set; }

        public WarehouseRepository(ILogger<WarehouseRepository> logger)
        {
            _logger = logger;

            Products = GetProducts();
            RetailerProducts = GetRetailerProducts();
        }

        private IEnumerable<Product> GetProducts()
        {
            if (Products != null) return Products;

            try
            {
                var path = ConfigurationManager.AppSettings["Products"];
                using var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{path}");
                var text = reader.ReadToEnd();
                var json = text.CsvToJson2();

                // Output Products JSON to Debug console
                Debug.WriteLine($"Products: {JToken.Parse(json).ToString(Formatting.Indented)}");

                Products = json.FromJson<IEnumerable<Product>>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Products;
        }

        private IEnumerable<RetailerProduct> GetRetailerProducts()
        {
            if (RetailerProducts != null) return RetailerProducts;

            try
            {
                var path = ConfigurationManager.AppSettings["RetailerProducts"];
                using var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{path}");
                var text = reader.ReadToEnd();
                var json = text.CsvToJson2();

                RetailerProducts = json.FromJson<IEnumerable<RetailerProduct>>()
                    .Where(o => o.ProductId != 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RetailerProducts;
        }
    }
}
