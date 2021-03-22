using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProductShop.Data;
using ProductShop.DataTransferObjects.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            var json = File.ReadAllText("./Datasets/users.xml");
            Console.WriteLine(ImportUsers(context, json));
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string root = "Users";
            var xmlSerializer = new XmlSerializer(typeof(List<ImportUsersDTO>),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var usersDTO = xmlSerializer.Deserialize(stringReader) as List<ImportUsersDTO>;
            var users = usersDTO
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
    }
}