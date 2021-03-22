using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using CarDealer.Data;
using CarDealer.DataTransferObjects.ExportModels;

namespace CarDealer
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            EnsureDirectoryExists();
            var resultXml = GetSalesWithAppliedDiscount(context);
            File.WriteAllText(OutputPath + "/sales-discounts.xml", resultXml);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .ToList()
                .Select(x => new SalesWithAppliedDiscountEM()
                {
                    Sales = x.Customer.Sales.Select(s => new SaleWithDiscountEM()
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = x.Car.TravelledDistance,
                            Discount = s.Discount,
                            CustomerName = s.Customer.Name,
                            Price = x.Car.PartCars.Sum(p => p.Part.Price),
                            PriceWithDiscount = (x.Car.PartCars.Sum(p => p.Part.Price))
                                                - ((x.Car.PartCars
                                                    .Sum(p => p.Part.Price) * s.Discount) / 100)
                        })
                        .ToList()
                })
                .ToList();
            const string root = "sales";
            var xmlSerializer = new XmlSerializer(typeof(List<SalesWithAppliedDiscountEM>),
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sales, namespaces);
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