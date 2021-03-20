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
            var resultXml = GetCarsWithTheirListOfParts(context);
            File.WriteAllText(OutputPath + "/cars-and-parts.xml", resultXml);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context
                .Cars
                .Select(x => new CarsEM
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars
                        .Select(pt => new PartsEM
                        {
                            Name = pt.Part.Name,
                            Price = pt.Part.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(p => p.Model)
                .Take(5)
                .ToArray();
            var root = "cars";
            var xmlSerializer = new XmlSerializer(typeof(CarsEM[]),
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, carsWithParts, namespaces);
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