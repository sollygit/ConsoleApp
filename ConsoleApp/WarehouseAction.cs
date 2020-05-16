using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace ConsoleApp
{
    static class WarehouseAction
    {
        public static void Products(ServiceProvider serviceProvider)
        {
            var warehouseService = serviceProvider.GetService<IWarehouseService>();
            var path = ConfigurationManager.AppSettings["Products"];
            var products = warehouseService.GetProducts(path);

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName}");
            }
        }

        public static void RetailerProducts(ServiceProvider serviceProvider)
        {
            var warehouseService = serviceProvider.GetService<IWarehouseService>();
            var path = ConfigurationManager.AppSettings["RetailerProducts"];
            var retailerProducts = warehouseService.GetRetailerProducts(path);

            foreach (var p in retailerProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.RetailerName},{p.RetailerProductCode},{p.RetailerProductCodeType},{p.DateReceived}");
            }
        }

        public static void OutputProducts(ServiceProvider serviceProvider)
        {
            var warehouseService = serviceProvider.GetService<IWarehouseService>();
            var outputProducts = warehouseService.GetOutputProducts();

            foreach (var p in outputProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName},{p.CodeType},{p.Code}");
            }
        }
    }
}
