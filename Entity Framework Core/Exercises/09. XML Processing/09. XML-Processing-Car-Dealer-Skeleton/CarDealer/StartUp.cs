using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
            var resultXml = GetCarsWithDistance(context);
            File.WriteAllText(OutputPath + "/cars.xml", resultXml);
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            const string root = "cars";
            var sortedCars = context
                .Cars
                .Where(x => x.TravelledDistance > 2_000_000)
                .Select(x => new CarWithDistanceEM()
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToList();
            var xmlSerializer = new XmlSerializer(typeof(List<CarWithDistanceEM>), 
                new XmlRootAttribute(root));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            var result = new StringBuilder();
            var writer = new StringWriter(result);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedCars, namespaces);
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