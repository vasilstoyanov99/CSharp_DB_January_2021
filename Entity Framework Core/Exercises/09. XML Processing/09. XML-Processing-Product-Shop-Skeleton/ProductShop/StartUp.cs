﻿using System;
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
            var jsonResult = GetProductsInRange(context);
            File.WriteAllText(OutputPath + "/products-in-range.xml", jsonResult);
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var sortedProducts = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductsInRangeExportModel
                {
                    Price = p.Price,
                    Name = p.Name,
                    BuyerName = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();
            const string root = "Products";
            var xmlSerializer = new XmlSerializer(typeof(List<ProductsInRangeExportModel>),
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedProducts, namespaces);
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