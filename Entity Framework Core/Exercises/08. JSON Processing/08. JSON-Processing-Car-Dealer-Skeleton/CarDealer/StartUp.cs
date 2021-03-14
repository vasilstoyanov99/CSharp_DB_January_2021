using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            ////Insert Suppliers Data 
            var jsonSuppliers = File.ReadAllText("../../../Datasets/suppliers.json");
            Console.WriteLine(ImportSuppliers(context, jsonSuppliers));
            

            ////Insert Parts Data
            //var jsonParts = File.ReadAllText("../../../Datasets/parts.json");
            //var parts = JsonConvert.DeserializeObject<Part[]>(jsonParts);
            //context.Parts.AddRange(parts);

            ////Insert Cars Data
            //var jsonCars = File.ReadAllText("../../../Datasets/cars.json");
            //var cars = JsonConvert.DeserializeObject<Car[]>(jsonCars);
            //context.Cars.AddRange(cars);

            ////Insert Customers Data
            //var jsonCustomers = File.ReadAllText("../../../Datasets/customers.json");
            //var customers = JsonConvert.DeserializeObject<Customer[]>(jsonCustomers);
            //context.Customers.AddRange(customers);

            ////Insert Sales Data
            //var jsonSales = File.ReadAllText("../../../Datasets/sales.json");
            //var sales = JsonConvert.DeserializeObject<Sale[]>(jsonSales);
            //context.Sales.AddRange(sales);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            var result = $"Successfully imported {suppliers.Count()}.";
            return result;
        }
    }
}