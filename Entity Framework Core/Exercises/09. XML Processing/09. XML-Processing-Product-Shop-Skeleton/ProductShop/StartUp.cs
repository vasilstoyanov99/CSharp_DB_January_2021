using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
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
            var jsonResult = GetSoldProducts(context);
            File.WriteAllText(OutputPath + "/users-sold-products.xml", jsonResult);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var sortedItems = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new UsersSoldProductsExportProducts()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(ps => new SoldProductExportModel()
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .ToArray()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();
            const string root = "Users";
            var xmlSerializer = new XmlSerializer(typeof(UsersSoldProductsExportProducts[]),
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedItems, namespaces);
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