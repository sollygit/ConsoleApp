using ConsoleApp.Common;
using ConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly IConfiguration configuration;
        private IEnumerable<Product> products;
        private IEnumerable<RetailerProduct> retailerProducts;

        public WarehouseSearchProvider SearchProvider { get; private set; }

        public WarehouseService(ILogger<WarehouseService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            InitWarehouse();
        }

        private void InitWarehouse()
        {
            try
            {
                var path = configuration["Products"];
                products = Deserializer.FromCsv<Product>(path, new string[] { "ProductId", "ProductName" });

                path = configuration["RetailerProducts"];
                retailerProducts = Deserializer.FromCsv<RetailerProduct>(path, new string[] { "ProductId","RetailerName","RetailerProductCode","RetailerProductCodeType","DateReceived" })
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
