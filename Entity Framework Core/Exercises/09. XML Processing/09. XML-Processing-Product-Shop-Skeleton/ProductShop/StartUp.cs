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
            var json = File.ReadAllText("./Datasets/categories-products.xml");
            Console.WriteLine(ImportCategoryProducts(context, json));
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            const string root = "CategoryProducts";
            var xmlSerializer = new XmlSerializer(typeof(List<CategoryProductImportModel>),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var categoryProductsDTOs = xmlSerializer.Deserialize(stringReader)
                as List<CategoryProductImportModel>;
            var categoryProducts = categoryProductsDTOs
                .Select(ct => new CategoryProduct()
                {
                    CategoryId = ct.CategoryId,
                    ProductId = ct.ProductId
                })
                .ToList();
            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count}";
        }
    }
}