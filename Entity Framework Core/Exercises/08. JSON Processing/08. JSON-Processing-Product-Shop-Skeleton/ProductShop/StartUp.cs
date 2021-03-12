using System.IO;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;

using ProductShop.Data;
using ProductShop.Data.DTO;

namespace ProductShop
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        
        public static void Main(string[] args)
        {
            InitializeMapper();
            var context = new ProductShopContext();
            var json = GetCategoriesByProductsCount(context);
            EnsureDirectoryExists();
            File.WriteAllText(OutputPath + "/categories-by-products.json", json);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var sortedCategories = context
                .Categories
                .OrderByDescending(x => x.CategoryProducts.Count)
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.CategoryProducts.Count,
                    averagePrice = x.CategoryProducts.Average(p => p.Product.Price).ToString("F2"),
                    totalRevenue = x.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F2"),
                })
                .ToList();

            var json = JsonConvert.SerializeObject(sortedCategories, Formatting.Indented);
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