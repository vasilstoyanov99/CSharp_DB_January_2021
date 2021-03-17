using System;
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
            var inputXml = File.ReadAllText("./Datasets/suppliers.xml");
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine(ImportSuppliers(context, inputXml));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierInputModel[]), 
                new XmlRootAttribute("Suppliers"));
            var stringReader = new StringReader(inputXml);
            var suppliersDTOs = xmlSerializer.Deserialize(stringReader) as SupplierInputModel[];
            var suppliers = suppliersDTOs.Select(x => new Supplier()
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
                .ToList();
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }
    }
}