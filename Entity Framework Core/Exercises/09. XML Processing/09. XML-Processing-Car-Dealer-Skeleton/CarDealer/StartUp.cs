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
            var inputXml = File.ReadAllText("./Datasets/cars.xml");
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            Console.WriteLine(ImportCars(context, inputXml));
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            const string root = "Cars";
            var xmlSerializer = new XmlSerializer(typeof(CarInputModel[]),
                new XmlRootAttribute(root));
            var stringReader = new StringReader(inputXml);
            var carDTOs = xmlSerializer.Deserialize(stringReader) as CarInputModel[];
            var partsIds = context
                .Parts
                .Select(x => x.Id)
                .ToList();
            var cars = new List<Car>();
            var partsCars = new List<PartCar>();

            foreach (var carDTO in carDTOs)
            {
                var car = new Car()
                {
                    Make = carDTO.Make,
                    Model = carDTO.Model,
                    TravelledDistance = carDTO.TraveledDistance,
                };

                var sortedParts = carDTO
                    .Parts
                    .Where(p => partsIds.Contains(p.Id))
                    .Select(p => p.Id)
                    .Distinct();

                foreach (var partIds in sortedParts)
                {
                    var partCar = new PartCar()
                    {
                        PartId = partIds,
                        Car = car
                    };
                    
                    partsCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.AddRange(cars);
            context.AddRange(partsCars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}";
        }
    }
}