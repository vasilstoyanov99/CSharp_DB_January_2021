using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoftJail.DataProcessor.ExportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var sortedPrisoners = context
                .Prisoners
                .Include(x => x.PrisonerOfficers)
                .ThenInclude(x => x.Officer)
                .ToList()
                .Where(p => ids.Contains(p.Id))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new PrisonerByCellExportModel()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(o => new OfficerExportModel()
                        {
                            Department = o.Officer.Department.Name,
                            OfficerName = o.Officer.FullName
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToList(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(o => o.Officer.Salary)
                })
                .ToList();

            var json = JsonConvert.SerializeObject(sortedPrisoners, Formatting.Indented);
            return json.Trim();

        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

            var sortedPrisonersInbox = context
                .Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(p => new PrisonersInboxExportModel()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails.Select(m => new MessageExportModel()
                        {
                            Description = ReverseString(m.Description)
                        })
                        .ToArray()
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();
            const string root = "Prisoners";
            var xmlSerializer = new XmlSerializer(typeof(PrisonersInboxExportModel[]),
                new XmlRootAttribute(root));
            var result = new StringBuilder();
            var writer = new StringWriter(result);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (writer)
            {
                xmlSerializer.Serialize(writer, sortedPrisonersInbox, namespaces);
            }

            return result.ToString().Trim();
        }

        private static string ReverseString(string toReverse)
        {
            var charArray = toReverse.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}