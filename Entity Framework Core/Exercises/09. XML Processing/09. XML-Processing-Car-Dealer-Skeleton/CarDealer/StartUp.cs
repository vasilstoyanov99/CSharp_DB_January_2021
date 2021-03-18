using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using CarDealer.Data;
using CarDealer.DataTransferObjects.ExportModels;

namespace CarDealer
{
    public class StartUp
    {
        private static string OutputPath = "../../../Datasets/Output";
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            EnsureDirectoryExists();
            var resultXml = GetLocalSuppliers(context);
            File.WriteAllText(OutputPath + "/local-suppliers.xml", resultXml);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            const string root = "suppliers";
            var sortedSuppliers = context
                .Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new LocalSupplierEM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToList();
            var xmlSerializer = new XmlSerializer(typeof(List<LocalSupplierEM>),
                new XmlRootAttribute(root));
            var result = new StringBuilder();
            var writer = new StringWriter(result);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedSuppliers, namespaces);
            }

            return result.ToString();
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