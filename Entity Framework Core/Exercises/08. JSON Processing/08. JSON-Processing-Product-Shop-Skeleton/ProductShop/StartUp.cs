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

        public static string GetSoldProducts(ProductShopContext context)
        {
            var sortedUsers = context
                .Users
                .Where(x => x.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u
                        .ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                        .ToList()
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
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