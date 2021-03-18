using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using CarDealer.Data;
using CarDealer.DataTransferObjects;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            var inputXml = File.ReadAllText("./Datasets/customers.xml");
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            Console.WriteLine(ImportSales(context, inputXml));
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            const string root = "Sales";
            var xmlSerializer = new XmlSerializer(typeof(SaleInputModel[]),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var saleDTOs = xmlSerializer.Deserialize(stringReader) as SaleInputModel[];
            var carIds = context
                .Cars
                .Select(c => c.Id)
                .ToList();
            var sales = saleDTOs
                .Where(x => carIds.Contains(x.CarId))
                .Select(s => new Sale()
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                })
                .ToList();
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}";
        }
    }
}