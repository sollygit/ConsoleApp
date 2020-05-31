using ConsoleApp.Common;
using ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                var text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\{path}");
                Products = Helper.DeserializeChoETL<Product>(path, new string[] { "ProductId", "ProductName" });
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
                var text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\{path}");
                RetailerProducts = Helper.DeserializeChoETL<RetailerProduct>(path, new string[] { 
                    "ProductId","RetailerName","RetailerProductCode","RetailerProductCodeType","DateReceived" })
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
