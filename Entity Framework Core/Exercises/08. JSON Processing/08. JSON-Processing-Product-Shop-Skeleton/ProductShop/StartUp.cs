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
            string json = File.ReadAllText("../../../Datasets/users.json");
            Console.WriteLine(ImportUsers(context, json));
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            var result = $"Successfully imported {users.Length}";
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