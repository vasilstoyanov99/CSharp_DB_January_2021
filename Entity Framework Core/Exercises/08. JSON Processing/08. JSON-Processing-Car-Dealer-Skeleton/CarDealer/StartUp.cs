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
        private static IMapper mapper;
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = config.CreateMapper();
            var context = new CarDealerContext();

            //Insert Customers Data
            var jsonCars = File.ReadAllText("../../../Datasets/customers.json");
            Console.WriteLine(ImportCustomers(context, jsonCars));

        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customersInputModels = JsonConvert
                .DeserializeObject<IEnumerable<CustomerInputModel>>(inputJson);
            var customers = mapper.Map<IEnumerable<Customer>>(customersInputModels);
            context.Customers.AddRange(customers);
            context.SaveChanges();
            var result = $"Successfully imported {customersInputModels.Count()}.";
            return result;
        }
    }
}