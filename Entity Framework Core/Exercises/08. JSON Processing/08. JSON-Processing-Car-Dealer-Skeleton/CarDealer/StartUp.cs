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
            var json = GetTotalSalesByCustomer(context);
            File.WriteAllText(OutputPath + "/customers-total-sales.json", json);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var sortedCustomers = context
                .Customers
                .Where(x => x.Sales.Any(s => s.Car != null))
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(x => x.Car != null),
                    spentMoney = c.Sales.Sum(x => x.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();
            var jsonResult = JsonConvert.SerializeObject(sortedCustomers, Formatting.Indented);
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