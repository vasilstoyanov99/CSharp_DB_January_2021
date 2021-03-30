using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using SoftJail.DataProcessor.ImportDto;

namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsCellsDTOs = JsonConvert
                .DeserializeObject<List<DepartmentsCellsImportModel>>(jsonString);

            var result = new StringBuilder();

            foreach (var departmentDTO in departmentsCellsDTOs)
            {
                if (!IsValid(departmentDTO) || !departmentDTO.Cells.All(IsValid) 
                                            || !departmentDTO.Cells.Any())
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department()
                {
                    Name = departmentDTO.Name,
                    Cells = departmentDTO.Cells.Select(c => new Cell()
                    {
                        CellNumber = c.CellNumber,
                        HasWindow = c.HasWindow
                    })
                        .ToList()
                };

                result.AppendLine($"Imported {department.Name}" +
                                  $" with {department.Cells.Count} cells");
                context.Departments.AddRange(department);
                context.Cells.AddRange(department.Cells);
                context.SaveChanges();
            }

            return result.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonersMailsDTOs = JsonConvert
                .DeserializeObject<List<PrisonersMailsImportModel>>(jsonString);
            var result = new StringBuilder();

            foreach (var prisonerMailDTO in prisonersMailsDTOs)
            {
                if (!IsValid(prisonerMailDTO) || !prisonerMailDTO.Mails.All(IsValid))
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var incarcerationDate = DateTime.ParseExact(prisonerMailDTO.IncarcerationDate,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var isValidReleaseDate = DateTime.TryParseExact(prisonerMailDTO.ReleaseDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime releaseDate);

                var prisoner = new Prisoner()
                {
                    FullName = prisonerMailDTO.FullName,
                    Nickname = prisonerMailDTO.Nickname,
                    Age = prisonerMailDTO.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = isValidReleaseDate ? (DateTime?)releaseDate : null,
                    Bail = prisonerMailDTO.Bail,
                    CellId = prisonerMailDTO.CellId,
                    Mails = prisonerMailDTO.Mails.Select(m => new Mail()
                        {
                            Description = m.Description,
                            Address = m.Address,
                            Sender = m.Sender
                        })
                        .ToList()
                };

                result.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                context.Prisoners.Add(prisoner);
                context.Mails.AddRange(prisoner.Mails);
                context.SaveChanges();
            }

            return result.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}