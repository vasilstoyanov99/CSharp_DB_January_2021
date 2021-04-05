using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using TeisterMask.DataProcessor.ExportDto;
using XmlFacade;

namespace TeisterMask.DataProcessor
{
    using System;

    using Data;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var sortedProjects = context
                .Projects
                .ToArray()
                .Where(p => p.Tasks.Any())
                .OrderByDescending(p => p.Tasks.Count) // should it be here
                .ThenBy(p => p.Name)
                .Select(p => new ProjectWithTheirTasksExportModel()
                {
                    ProjectName = p.Name,
                    TasksCount = p.Tasks.Count().ToString(),
                    HasEndDate = p.DueDate.HasValue ? "Yes" : "No", // check for bugs
                    Tasks = p.Tasks.Select(t => new TaskXmlExportModel()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .ToArray();

            var xmlResult = XmlConverter.Serialize(sortedProjects, "Projects");
            return xmlResult;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var sortedEmployees = context
                .Employees
                .ToArray()
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks
                    .Where((t => t.Task.OpenDate >= date)).Count())
                .ThenBy(e => e.Username)
                .Select(e => new
                {
                    e.Username,
                    Tasks = e.EmployeesTasks.Where(et => et.Task.OpenDate >= date) //a bug?
                        .OrderByDescending(et => et.Task.DueDate) // should it be here?
                        .ThenBy(et => et.Task.Name) // should it be here?
                        .Select(et => new
                        {
                            TaskName = et.Task.Name,
                            OpenDate = et.Task.OpenDate
                                .ToString("d", CultureInfo.InvariantCulture),
                            DueDate = et.Task.DueDate
                                .ToString("d", CultureInfo.InvariantCulture),
                            LabelType = et.Task.LabelType.ToString(),
                            ExecutionType = et.Task.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .Take(10)
                 // should it be here? should I add where
                 // // should it be here?
                .ToArray();

            var jsonResult = JsonConvert.SerializeObject(sortedEmployees, Formatting.Indented);
            return jsonResult;
        }
    }
}