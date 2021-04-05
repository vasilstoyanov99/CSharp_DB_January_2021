using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;
using XmlFacade;
using Task = TeisterMask.Data.Models.Task;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var projectDTOs = XmlConverter.Deserializer<ProjectTasksImportModel>(xmlString,
                "Projects");

            var result = new StringBuilder();

            foreach (var projectDTO in projectDTOs)
            {
                if (!IsValid(projectDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dueDate;
                bool isDueDateValid = DateTime.TryParseExact(projectDTO.DueDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate);

                DateTime openDate;
                bool isOpenDateValid = DateTime.TryParseExact(projectDTO.OpenDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out openDate);

                if (!isOpenDateValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project()
                {
                    Name = projectDTO.Name,
                    OpenDate = openDate
                };

                if (!isDueDateValid)
                {
                    project.DueDate = null;
                }
                else
                {
                    project.DueDate = dueDate;
                }


                foreach (var taskDTO in projectDTO.Tasks)
                {
                    if (!IsValid(taskDTO))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskDueDate;
                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDTO.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDate);

                    DateTime taskOpenDate;
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDTO.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out taskOpenDate);

                    if (!isTaskDueDateValid || !isTaskOpenDateValid)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (taskOpenDate < project.OpenDate
                        || taskDueDate > project.DueDate) // TODO: might have some bugs
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task()
                    {
                        Name = taskDTO.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        LabelType = Enum.Parse<LabelType>(taskDTO.LabelType), // should be int
                        ExecutionType = Enum.Parse<ExecutionType>(taskDTO.ExecutionType)
                    };

                    project.Tasks.Add(task);
                }

                context.Projects.Add(project);
                context.SaveChanges();
                result.AppendLine(String.Format(SuccessfullyImportedProject, project.Name,
                    project.Tasks.Count));
            }

            return result.ToString().Trim();

            //TODO: Check DateTime? DueDate on project
            //I dont have to parse the project OpenDate as well as the dates on Task
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeDTOs = JsonConvert
                .DeserializeObject<List<EmployeesImportModel>>(jsonString);

            var result = new StringBuilder();

            foreach (var employeeDTO in employeeDTOs)
            {
                if (!IsValid(employeeDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                //unique tasks

                var tasks = new List<Task>();
                var employeeTasks = new List<EmployeeTask>();

                var employee = new Employee()
                {
                    Username = employeeDTO.Username,
                    Email = employeeDTO.Email,
                    Phone = employeeDTO.Phone
                };

                foreach (var taskId in employeeDTO.Tasks)
                {
                    var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);

                    if (task == null)
                    {
                        result.AppendLine(ErrorMessage);
                        continue; //TODO: what if all tasks are null?
                    }

                    if (!tasks.Contains(task))
                    {
                        tasks.Add(task);
                        var newEmployeeTask = new EmployeeTask()
                        {
                            Employee = employee,
                            Task = task
                        };
                        employeeTasks.Add(newEmployeeTask);
                    }
                }

                employee.EmployeesTasks = employeeTasks;

                result.AppendLine(String.Format(SuccessfullyImportedEmployee,
                    employee.Username, employee.EmployeesTasks.Count));
                context.Employees.Add(employee);
                context.SaveChanges();
            }

            return result.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}