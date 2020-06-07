using ConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Common
{
    public class WarehouseSearchProvider
    {
        private readonly IEnumerable<Product> products;
        private readonly IEnumerable<RetailerProduct> retailerProducts;

        public WarehouseSearchProvider(IEnumerable<Product> products, IEnumerable<RetailerProduct> retailerProducts)
        {
            this.products = products;
            this.retailerProducts = retailerProducts;
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
            return map.Values.AsEnumerable();
        }
    }
}
