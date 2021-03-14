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
            var json = GetCarsFromMakeToyota(context);
            File.WriteAllText(OutputPath + "/toyota-cars.json", json);

        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var sortedCars = context
                .Cars
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new CarDTO()
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(sortedCars, Formatting.Indented);
            return jsonResult;
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