using System;
using System.IO;
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
            //ResetDatabase(context);
            string json = File.ReadAllText("../../../Datasets/products.json");
            Console.WriteLine(ImportProducts(context, json));
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            context.Products.AddRange(products);
            var result = $"Successfully imported {products.Length}";
            context.SaveChanges();
            return result;
        }

        //private static void ResetDatabase(ProductShopContext context)
        //{
        //    if (context.Database.EnsureDeleted())
        //    {
        //        Console.WriteLine("Database successfully deleted!");
        //    }

        //    if (context.Database.EnsureCreated())
        //    {
        //        Console.WriteLine("Database successfully created!");
        //    }
        //}
    }
}