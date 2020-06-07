using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Actions
{
    static class Warehouse
    {
        public static async Task Products()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var products = await warehouseService.GetProducts();

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName}");
            }
        }

        public static async Task RetailerProducts()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var retailerProducts = await warehouseService.GetRetailerProducts();

            foreach (var p in retailerProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.RetailerName},{p.RetailerProductCode},{p.RetailerProductCodeType},{p.DateReceived}");
            }
        }

        public static async Task OutputProducts()
        {
            var warehouseService = Program.ServiceProvider.GetService<IWarehouseService>();
            var outputProducts = await warehouseService.GetOutputProducts();

            foreach (var p in outputProducts)
            {
                Console.WriteLine($"{p.ProductId},{p.ProductName},{p.CodeType},{p.Code}");
            }
        }
    }
}
