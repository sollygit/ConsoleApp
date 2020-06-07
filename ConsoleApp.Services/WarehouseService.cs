using ConsoleApp.Common;
using ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly ILogger logger;
        private IEnumerable<Product> products;
        private IEnumerable<RetailerProduct> retailerProducts;

        public WarehouseService(ILogger<WarehouseService> logger)
        {
            this.logger = logger;
            products = GetProducts();
            retailerProducts = GetRetailerProducts();
        }

        public IEnumerable<Product> GetProducts()
        {
            if (products != null) return products;

            try
            {
                var path = ConfigurationManager.AppSettings["Products"];
                var text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\{path}");
                products = Helper.Deserialize<Product>(path, new string[] { "ProductId", "ProductName" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return products;
        }

        public IEnumerable<RetailerProduct> GetRetailerProducts()
        {
            if (retailerProducts != null) return retailerProducts;

            try
            {
                var path = ConfigurationManager.AppSettings["RetailerProducts"];
                var text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\{path}");
                retailerProducts = Helper.Deserialize<RetailerProduct>(path, new string[] {
                    "ProductId","RetailerName","RetailerProductCode","RetailerProductCodeType","DateReceived" })
                    .Where(o => o.ProductId != 0);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return retailerProducts;
        }

        public IEnumerable<OutputProduct> GetOutputProducts()
        {
            var map = new Dictionary<string, OutputProduct>();

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

                if (!map.ContainsKey(key))
                {
                    map.Add(key, outputProduct);
                }
                else
                {
                    // Ensure we keep the latest DateReceived
                    if (map[key].DateReceived < retail.DateReceived)
                    {
                        map[key].DateReceived = retail.DateReceived;
                        map[key].Code = retail.RetailerProductCode;
                    }
                }
            }
            return map.Values.ToList();
        }
    }
}
