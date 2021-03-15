using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var json = GetSalesWithAppliedDiscount(context);
            File.WriteAllText(OutputPath + "/sales-discounts.json", json);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sortedSales = context
                .Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    price = s.Car.PartCars.Sum(x => x.Part.Price).ToString("F2"),
                    priceWithDiscount = s.Discount <= 0
                        ? s.Car.PartCars.Sum(p => p.Part.Price).ToString("F2")
                        : ((s.Car.PartCars.Sum(p => p.Part.Price)) -
                          (s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100)).ToString("F2")
                })
                .Take(10)
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(sortedSales, Formatting.Indented);
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