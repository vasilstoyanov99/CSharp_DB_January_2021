using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using CarDealer.Data;
using CarDealer.DataTransferObjects.ExportModels;
using Microsoft.EntityFrameworkCore;

namespace CarDealer
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            EnsureDirectoryExists();
            var resultXml = GetTotalSalesByCustomer(context);
            File.WriteAllText(OutputPath + "/customers-total-sales.xml", resultXml);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var sortedCustomers = context
                .Customers
                .Include(s => s.Sales)
                .ThenInclude(c => c.Car)
                .ThenInclude(pt => pt.PartCars)
                .ThenInclude(p => p.Part)
                .ToList()
                .Select(c => new TotalSalesByCustomerEM()
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pt => pt.Part.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToList();
            const string root = "customers";
            var xmlSerializer = new XmlSerializer(typeof(List<TotalSalesByCustomerEM>),
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedCustomers, namespaces);
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