using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            string json = File.ReadAllText("../../../Datasets/categories.json");
            Console.WriteLine(ImportCategories(context, json));
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null)
                .ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();
            var result = $"Successfully imported {categories.Count}";
            return result;
        }
    }
}