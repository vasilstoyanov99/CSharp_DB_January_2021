using System;
using System.Collections.Generic;
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
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            //Insert Cars Data
            var jsonCars = File.ReadAllText("../../../Datasets/cars.json");
            Console.WriteLine(ImportCars(context, jsonCars));

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarInputModel>>(inputJson);
            var listOfCars = new List<Car>();

            foreach (var car in cars)
            {
                var currentCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                foreach (var partId in car?.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar()
                    {
                        PartId = partId
                    });
                }

                listOfCars.Add(currentCar);
            }

            context.Cars.AddRange(listOfCars);
            context.SaveChanges();
            var result = $"Successfully imported {listOfCars.Count()}.";
            return result;
        }
    }
}