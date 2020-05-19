using ConsoleApp.Models;
using ConsoleApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Service
{
    public interface IWarehouseService
    {
        IEnumerable<Product> ProductsAll();
        IEnumerable<RetailerProduct> RetailerProductsAll();
        IEnumerable<OutputProduct> OutputProducts();
    }

    public class WarehouseService : IWarehouseService
    {
        readonly IWarehouseRepository _warehouseRepository;
        
        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;            
        }

        public IEnumerable<Product> ProductsAll()
        {
            return _warehouseRepository.Products;
        }

        public IEnumerable<RetailerProduct> RetailerProductsAll()
        {
            return _warehouseRepository.RetailerProducts;
        }

        public IEnumerable<OutputProduct> OutputProducts()
        {
            var map = new Dictionary<string, OutputProduct>();

            foreach (var retail in _warehouseRepository.RetailerProducts)
            {
                // Get the master product
                var master = _warehouseRepository.Products.Where(o => o.ProductId == retail.ProductId).SingleOrDefault();

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
