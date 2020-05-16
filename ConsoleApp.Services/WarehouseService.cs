using ConsoleApp.Common;
using ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleApp.Service
{
    public interface IWarehouseService
    {
        IEnumerable<Product> GetProducts(string path);
        IEnumerable<RetailerProduct> GetRetailerProducts(string path);
        IEnumerable<OutputProduct> GetOutputProducts();
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger _logger;

        public IEnumerable<Product> Products { get; internal set; }
        public IEnumerable<RetailerProduct> RetailerProducts { get; internal set; }

        public WarehouseService(ILogger<WarehouseService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Product> GetProducts(string path)
        {
            try
            {
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

        public IEnumerable<RetailerProduct> GetRetailerProducts(string path)
        {
            try
            {
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

        public IEnumerable<OutputProduct> GetOutputProducts()
        {
            var dictionary = new Dictionary<string, OutputProduct>();

            foreach (var retail in RetailerProducts)
            {
                // Get the master product
                var master = Products.Where(o => o.ProductId == retail.ProductId).SingleOrDefault();

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
