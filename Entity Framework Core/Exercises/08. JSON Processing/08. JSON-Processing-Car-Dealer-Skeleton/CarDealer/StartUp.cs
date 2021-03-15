using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            EnsureDirectoryExists();
            var json = GetCarsWithTheirListOfParts(context);
            File.WriteAllText(OutputPath + "/cars-and-parts.json", json);

        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context
                .Cars
                .Select(c => new OnlyCarDTO()
                {
                    Car = new CarAndPartsDTO()
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance,
                        Parts = c.PartCars.Select(y => new PartDTO()
                        {
                            Name = y.Part.Name,
                            Price = y.Part.Price.ToString("f2")
                        })

                    }
                })
                .ToList();

            var resultJson = JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);
            return resultJson;
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