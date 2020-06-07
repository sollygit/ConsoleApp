using ConsoleApp.Common;
using ConsoleApp.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Service
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<RetailerProduct>> GetRetailerProducts();
        Task<IEnumerable<OutputProduct>> GetOutputProducts();
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly ILogger logger;
        private IEnumerable<Product> products;
        private IEnumerable<RetailerProduct> retailerProducts;

        public WarehouseSearchProvider SearchProvider { get; private set; }

        public WarehouseService(ILogger<WarehouseService> logger)
        {
            this.logger = logger;
            InitWarehouse();
        }

        private void InitWarehouse()
        {
            try
            {
                var path = ConfigurationManager.AppSettings["Products"];
                products = Helper.Deserialize<Product>(path, new string[] { "ProductId", "ProductName" });

                path = ConfigurationManager.AppSettings["RetailerProducts"];
                retailerProducts = Helper.Deserialize<RetailerProduct>(path, new string[] {
                    "ProductId","RetailerName","RetailerProductCode","RetailerProductCodeType","DateReceived" })
                    .Where(o => o.ProductId != 0);

                SearchProvider = new WarehouseSearchProvider(products, retailerProducts);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return Task.FromResult(products);
        }

        public Task<IEnumerable<RetailerProduct>> GetRetailerProducts()
        {
            return Task.FromResult(retailerProducts);
        }

        public Task<IEnumerable<OutputProduct>> GetOutputProducts()
        {
            return Task.FromResult(SearchProvider.GetOutputProducts());
        }
    }
}
