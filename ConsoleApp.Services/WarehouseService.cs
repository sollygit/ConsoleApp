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

namespace ConsoleApp.Service
{
    public interface IWarehouseService
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<RetailerProduct> GetRetailerProducts();
        IEnumerable<OutputProduct> GetOutputProducts();
    }

    public class WarehouseService : IWarehouseService
    {
        readonly ILogger _logger;
        IEnumerable<Product> products;
        IEnumerable<RetailerProduct> retailerProducts;

        public WarehouseService(ILogger<WarehouseService> logger)
        {
            _logger = logger;
            InitWarehouse();
        }

        public void InitWarehouse()
        {
            GetProducts();
            GetRetailerProducts();
        }

        public IEnumerable<Product> GetProducts()
        {
            if (products != null) return products;

            try
            {
                var path = ConfigurationManager.AppSettings["Products"];
                using var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{path}");
                var text = reader.ReadToEnd();
                var json = text.CsvToJson2();

                // Output Products JSON to Debug console
                Debug.WriteLine($"Products: {JToken.Parse(json).ToString(Formatting.Indented)}");

                products = json.FromJson<IEnumerable<Product>>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return products;
        }

        public IEnumerable<RetailerProduct> GetRetailerProducts()
        {
            if (retailerProducts != null) return retailerProducts;

            try
            {
                var path = ConfigurationManager.AppSettings["RetailerProducts"];
                using var reader = new StreamReader($"{Directory.GetCurrentDirectory()}\\{path}");
                var text = reader.ReadToEnd();
                var json = text.CsvToJson2();

                retailerProducts = json.FromJson<IEnumerable<RetailerProduct>>()
                    .Where(o => o.ProductId != 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return retailerProducts;
        }

        public IEnumerable<OutputProduct> GetOutputProducts()
        {
            var dictionary = new Dictionary<string, OutputProduct>();

            foreach (var retail in retailerProducts)
            {
                // Get the master product
                var master = products.Where(o => o.ProductId == retail.ProductId).SingleOrDefault();

                var outputProduct = new OutputProduct()
                {
                    ProductId = retail.ProductId,
                    ProductName = master.ProductName,
                    CodeType = retail.RetailerProductCodeType,
                    Code = retail.RetailerProductCode,
                    DateReceived = retail.DateReceived
                };

                // Create a unique dictionary key: ProductId-CodeType
                var key = $"{outputProduct.ProductId}-{outputProduct.CodeType}";

                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, outputProduct);
                }
                else
                {
                    // Ensure we keep the latest DateReceived
                    if (dictionary[key].DateReceived < retail.DateReceived)
                    {
                        dictionary[key].DateReceived = retail.DateReceived;
                        dictionary[key].Code = retail.RetailerProductCode;
                    }
                }
            }

            return dictionary.Values.ToList();
        }
    }
}
