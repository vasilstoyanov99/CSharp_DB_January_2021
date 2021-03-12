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
            var json = GetUsersWithProducts(context);
            EnsureDirectoryExists();
            File.WriteAllText(OutputPath + "/users-and-products.json", json);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var sortedUsers = context
                .Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(x => x.ProductsSold.Any(a => a.Buyer != null))
                .OrderByDescending(x => x.ProductsSold.Count(p => p.Buyer != null))
                .Select(u => new UserExportDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsExportDTO()
                    {
                        Count = u.ProductsSold.Count(p => p.Buyer != null),
                        Products = u.ProductsSold
                            .ToList()
                            .Where(p => p.Buyer != null)
                            .Select(p => new ProductDTO()
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .ToList()
                    }
                })
                .ToList();
            var result = new
            {
                count = sortedUsers.Count,
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