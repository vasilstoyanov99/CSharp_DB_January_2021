using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.DataTransferObjects.Export;

namespace ProductShop
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            EnsureDirectoryExists();
            var jsonResult = GetUsersWithProducts(context);
            File.WriteAllText(OutputPath + "/users-and-products.xml", jsonResult);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var sortedCategories = context
                .Categories
                .Select(c => new CategoriesByProductsCountExportModels()
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();
            const string root = "Categories";
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            var xmlSerializer = new XmlSerializer(typeof
                    (List<CategoriesByProductsCountExportModels>),
                new XmlRootAttribute(root));
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedCategories, namespaces);
            }

            return result.ToString();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var sortedUsers = new UsersCountExportModel
            {
                Count = context.Users.Count(u => u.ProductsSold
                    .Any(b => b.BuyerId != null)),
                Users = context
                    .Users
                    .Include(x => x.ProductsSold)
                    .ToArray()
                    .Where(u => u.ProductsSold.Count >= 1)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .Select(u => new UsersAndProductsExportModel()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new SoldProductExportModel()
                        {
                            Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                            Products = u.ProductsSold
                                .Select(ps => new ProductExportModel()
                                {
                                    Name = ps.Name,
                                    Price = ps.Price
                                })
                                .OrderByDescending(ps => ps.Price)
                                .Take(10)
                                .ToArray()
                        }
                    })
                    .ToArray()
            };
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            const string root = "Users";
            var xmlSerializer = new XmlSerializer(typeof(UsersCountExportModel),
                new XmlRootAttribute(root));
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedUsers, namespaces);
            }

            return result.ToString();
        }

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }
    }
}