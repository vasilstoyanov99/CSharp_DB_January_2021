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
            var json = GetSoldProducts(context);
            EnsureDirectoryExists();
            File.WriteAllText(OutputPath + "/users-sold-products.json", json);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var sortedUsers = context
                .Users
                .Where(x => x.ProductsSold.Count >= 1
                           /* && x.ProductsBought.Any(y => y.Buyer != null)*/)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new SoldProductsDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u
                        .ProductsSold
                        .Select(p => new ProductDto()
                    {
                            Name = p.Name,
                            Price = p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName
                    })
                        .ToList()
                })
                .ToList();
            var json = JsonConvert.SerializeObject(sortedUsers, Formatting.Indented);
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