using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            File.WriteAllText(OutputPath + "users-sold-products.json", json);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var sortedUsers = context
                .Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(x => x.ProductsSold.Any(a => a.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Where(x => x.BuyerId != null).Count(),
                        products = u.ProductsSold.Where(x => x.BuyerId != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                    }
                })
                .OrderByDescending(x => x.soldProducts.products.Count())
                .ToList();
            var result = new
            {
                usersCount = sortedUsers.Count(),
                users = sortedUsers
            };

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(result, settings);
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