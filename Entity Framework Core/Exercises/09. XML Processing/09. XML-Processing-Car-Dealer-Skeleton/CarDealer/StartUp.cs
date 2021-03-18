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
            Console.WriteLine(ImportCustomers(context, inputXml));
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            const string root = "Customers";
            var xmlSerializer = new XmlSerializer(typeof(CustomerInputModel[]),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var customerDTOs = xmlSerializer.Deserialize(stringReader) as CustomerInputModel[];
            var customers = customerDTOs
                .Select(c => new Customer()
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}";
        }
    }
}