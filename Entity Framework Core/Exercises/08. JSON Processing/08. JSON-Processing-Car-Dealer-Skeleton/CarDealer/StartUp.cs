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
            var json = GetLocalSuppliers(context);
            File.WriteAllText(OutputPath + "/local-suppliers.json", json);

        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var sortedSuppliers = context
                .Suppliers
                .Where(x => x.IsImporter == false)
                .Select(s => new SupplierDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();
            var resultJson = JsonConvert.SerializeObject(sortedSuppliers, Formatting.Indented);
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