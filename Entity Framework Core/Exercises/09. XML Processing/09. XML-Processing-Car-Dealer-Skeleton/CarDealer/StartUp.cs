using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Serialization;
using AutoMapper;
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
            var inputXml = File.ReadAllText("./Datasets/parts.xml");
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            Console.WriteLine(ImportParts(context, inputXml));
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            const string root = "Parts";
            var xmlSerializer = new XmlSerializer(typeof(PartInputModel[]), 
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var partsDTOs = xmlSerializer.Deserialize(stringReader) as PartInputModel[];
            var suppliersIds = context
                .Suppliers
                .Select(x => x.Id)
                .ToList();
            var parts = partsDTOs
                .Where(x => suppliersIds.Contains(x.SupplierId))
                .Select(x => new Part()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                })
                .ToList();
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count}";
        }
    }
}