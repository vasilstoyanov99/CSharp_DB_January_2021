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
            var resultXml = GetCarsFromMakeBmw(context);
            File.WriteAllText(OutputPath + "/bmw-cars.xml", resultXml);
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            const string root = "cars";
            var onlyBMWCars = context
                .Cars
                .Where(x => x.Make == "BMW")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new OnlyBMWCarEM()
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToList();
            var xmlSerializer = new XmlSerializer(typeof(List<OnlyBMWCarEM>),
                new XmlRootAttribute(root));
            var result = new StringBuilder();
            var writer = new StringWriter(result);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (writer)
            {
                xmlSerializer.Serialize(writer, onlyBMWCars, namespaces);
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