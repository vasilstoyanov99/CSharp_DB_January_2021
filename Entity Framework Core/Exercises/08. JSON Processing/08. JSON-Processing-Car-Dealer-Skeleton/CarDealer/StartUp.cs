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

            //Insert Parts Data
            var jsonSuppliers = File.ReadAllText("../../../Datasets/parts.json");
            Console.WriteLine(ImportParts(context, jsonSuppliers));

        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(x => x.SupplierId >= 1 && x.SupplierId < 32);
            context.Parts.AddRange(parts);
            context.SaveChanges();
            var result = $"Successfully imported {parts.Count()}.";
            return result;
        }
    }
}