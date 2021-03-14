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
        //private static IMapper mapper;
        public static void Main(string[] args)
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<CarDealerProfile>();
            //});
            //mapper = config.CreateMapper();
            var context = new CarDealerContext();

            //Insert Customers Data
            var jsonCars = File.ReadAllText("../../../Datasets/sales.json");
            Console.WriteLine(ImportSales(context, jsonCars));

        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);
            context.Sales.AddRange(sales);
            context.SaveChanges();
            var result = $"Successfully imported {sales.Length}.";
            return result;
        }
    }
}