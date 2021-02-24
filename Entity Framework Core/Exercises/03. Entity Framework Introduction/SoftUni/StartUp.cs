using System;
using System.Linq;
using System.Text;
using SoftUni.Data;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
        }

        //public static string GetEmployeesFullInformation(SoftUniContext context)
        //{
        //    var employees = context.Employees.
        //        Select( x => new
        //        {
        //            x.FirstName,
        //            x.LastName,
        //            x.MiddleName,
        //            x.JobTitle,
        //            x.Salary,
        //            x.EmployeeId
        //        }).
        //        OrderBy(e => e.EmployeeId).ToList();
        //    StringBuilder result = new StringBuilder();

        //    foreach (var e in employees)
        //    {
        //        result.AppendLine($"{e.FirstName} {e.LastName} " +
        //                          $"{e.MiddleName} {e.JobTitle} {e.Salary:f2}");
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        //{
        //    var sortedEmployees = context.Employees.
        //        Select(x => new
        //        {
        //            x.FirstName, 
        //            x.Salary
        //        }).
        //        Where(x => x.Salary > 50000).
        //        OrderBy(x => x.FirstName).ToList();

        //    StringBuilder result = new StringBuilder();

        //    foreach (var e in sortedEmployees)
        //    {
        //        result.AppendLine($"{e.FirstName} - {e.Salary:f2}");
        //    }

        //    return result.ToString().TrimEnd();
        //}

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sortedEmployees = context.Employees
                .Where(x =>
                    x.Department.Name == "Research and Development")
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    DepartmentName = x.Department.Name,
                    x.Salary
                })
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var e in sortedEmployees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} " +
                                  $"from {e.DepartmentName} - ${e.Salary:f2}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
