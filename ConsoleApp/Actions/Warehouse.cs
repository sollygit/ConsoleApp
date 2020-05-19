using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp.Actions
{
    static class Warehouse
    {
        public static void Products()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var products = warehouseService.ProductsAll();

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName}");
            }
        }

        public static void RetailerProducts()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var retailerProducts = warehouseService.RetailerProductsAll();

            foreach (var p in retailerProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.RetailerName},{p.RetailerProductCode},{p.RetailerProductCodeType},{p.DateReceived}");
            }
        }

        public static void OutputProducts()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var outputProducts = warehouseService.OutputProducts();

            foreach (var p in outputProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName},{p.CodeType},{p.Code}");
            }
        }
    }
}
