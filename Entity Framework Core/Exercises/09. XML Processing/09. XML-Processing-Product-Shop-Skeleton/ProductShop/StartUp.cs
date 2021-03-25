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
            var jsonResult = GetCategoriesByProductsCount(context);
            File.WriteAllText(OutputPath + "/categories-by-products.xml", jsonResult);
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

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }
    }
}