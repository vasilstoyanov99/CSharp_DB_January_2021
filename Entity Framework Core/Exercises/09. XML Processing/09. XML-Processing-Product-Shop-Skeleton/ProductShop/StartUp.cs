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
            var json = File.ReadAllText("./Datasets/categories.xml");
            Console.WriteLine(ImportCategories(context, json));
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string root = "Categories";
            var xmlSerializer = new XmlSerializer(typeof(List<CategoryImportModel>),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var categoriesDTOs = xmlSerializer.Deserialize(stringReader)
                as List<CategoryImportModel>;
            var categories = categoriesDTOs
                .Select(c => new Category
                {
                    Name = c.Name
                })
                .ToList();
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }
    }
}