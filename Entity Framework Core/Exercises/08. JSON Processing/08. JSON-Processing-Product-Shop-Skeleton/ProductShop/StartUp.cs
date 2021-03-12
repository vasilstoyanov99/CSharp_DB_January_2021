using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;

using ProductShop.Data;
using ProductShop.Data.DTO.ProductsDTO;

namespace ProductShop
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        
        public static void Main(string[] args)
        {
            InitializeMapper();
            var context = new ProductShopContext();
            var json = GetProductsInRange(context);
            EnsureDirectoryExists();
            File.WriteAllText(OutputPath + "/products-in-range.json", json);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var sortedProducts = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .ProjectTo<ProductsInRange>()
                .ToList();
           var json = JsonConvert.SerializeObject(sortedProducts, Formatting.Indented);
           return json;
        }

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }

         private static void InitializeMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<ProductShopProfile>();
            });
        }
    }
}