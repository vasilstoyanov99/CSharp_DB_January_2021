using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
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

        //public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        //{
        //    var sortedEmployees = context.Employees
        //        .Where(x =>
        //            x.Department.Name == "Research and Development")
        //        .Select(x => new
        //        {
        //            x.FirstName,
        //            x.LastName,
        //            DepartmentName = x.Department.Name,
        //            x.Salary
        //        })
        //        .OrderBy(x => x.Salary)
        //        .ThenByDescending(x => x.FirstName)
        //        .ToList();

        //    StringBuilder result = new StringBuilder();

        //    foreach (var e in sortedEmployees)
        //    {
        //        result.AppendLine($"{e.FirstName} {e.LastName} " +
        //                          $"from {e.DepartmentName} - ${e.Salary:f2}");
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string AddNewAddressToEmployee(SoftUniContext context)
        //{
        //    var address = new Address
        //    {
        //        AddressText = "Vitoshka 15",
        //        TownId = 4
        //    };

        //    var employee = context
        //        .Employees
        //        .FirstOrDefault(x => x.LastName == "Nakov");
        //    address.Employees.Add(employee);
        //    context.Addresses.Add(address);
        //    context.SaveChanges();
        //    employee.AddressId = null;
        //    employee.AddressId = address.AddressId;
        //    context.SaveChanges();

        //    var employeesAddressText = context
        //        .Employees
        //        .Select(x => new
        //        {
        //            x.Address.AddressText,
        //            x.Address.AddressId
        //        })
        //        .OrderByDescending(x => x.AddressId)
        //        .Take(10)
        //        .ToList();

        //    StringBuilder result = new StringBuilder();

        //    foreach (var e in employeesAddressText)
        //    {
        //        result.AppendLine(e.AddressText);
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetEmployeesInPeriod(SoftUniContext context)
        //{
        //    var sortedEmployees = context
        //        .Employees.Include(x => x.EmployeesProjects)
        //        .ThenInclude(x => x.Project)
        //        .Where(x =>
        //            x.EmployeesProjects
        //                .Any(p =>
        //                    p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
        //        .Select(x => new
        //        {
        //            EmployeeFirstName = x.FirstName,
        //            EmployeeLastName = x.LastName,
        //            ManagerFirstName = x.Manager.FirstName,
        //            ManagerLastName = x.Manager.LastName,
        //            Projects = x.EmployeesProjects.Select(p => new
        //            {
        //                ProjectName = p.Project.Name,
        //                StartDate = p.Project.StartDate,
        //                EndDate = p.Project.EndDate
        //            })
        //        })
        //        .Take(10)
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var e in sortedEmployees)
        //    {
        //        result.AppendLine($"{e.EmployeeFirstName} {e.EmployeeLastName} - " +
        //                          $"Manager: {e.ManagerFirstName} {e.ManagerLastName}");

        //        foreach (var p in e.Projects)
        //        {
        //            var endDate = p.EndDate.HasValue 
        //                ? p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) 
        //                : "not finished";

        //            var startDate = p.StartDate
        //                .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

        //            result.AppendLine($"--{p.ProjectName} - {startDate} - {endDate}");
        //        }
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetAddressesByTown(SoftUniContext context)
        //{
        //    var sortedAddresses = context
        //        .Addresses.Select(x => new
        //        {
        //            x.AddressText,
        //            TownName = x.Town.Name,
        //            EmployeesCount = x.Employees.Count
        //        })
        //        .OrderByDescending(x => x.EmployeesCount)
        //        .ThenBy(x => x.TownName)
        //        .ThenBy(x => x.AddressText)
        //        .Take(10)
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var address in sortedAddresses)
        //    {
        //        result.AppendLine($"{address.AddressText}, {address.TownName} - " +
        //                          $"{address.EmployeesCount} employees");
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetEmployee147(SoftUniContext context)
        //{
        //    var employeeData = context
        //        .Employees
        //        .Where(x => x.EmployeeId == 147)
        //        .Select(x => new
        //        {
        //            Name = $"{x.FirstName} {x.LastName}",
        //            x.JobTitle,
        //            Projects = x.EmployeesProjects
        //                .Select(p => new
        //                {
        //                    Name = p.Project.Name
        //                })
        //        })
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var e in employeeData)
        //    {
        //        result.AppendLine($"{e.Name} - {e.JobTitle}");

        //        foreach (var p in e.Projects.OrderBy(x => x.Name))
        //        {
        //            result.AppendLine(p.Name);
        //        }
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        //{
        //    var sortedDepartments = context
        //        .Departments
        //        .Where(x => x.Employees.Count > 5)
        //        .OrderBy(x => x.Employees.Count)
        //        .ThenBy(x => x.Name)
        //        .Select(x => new
        //        {
        //            x.Name,
        //            ManagerFirstName = x.Manager.FirstName,
        //            ManagerLastName = x.Manager.LastName,
        //            EmployeesData = x.Employees.Select(ed => new
        //            {
        //                FirstName = ed.FirstName,
        //                LastName = ed.LastName,
        //                JobTitle = ed.JobTitle
        //            })
        //            .OrderBy(e => e.FirstName)
        //            .ThenBy(e => e.LastName)
        //            .ToList()
        //        })
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var d in sortedDepartments)
        //    {
        //        result.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");

        //        foreach (var e in d.EmployeesData)
        //        {
        //            result.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
        //        }
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string GetLatestProjects(SoftUniContext context)
        //{
        //    var sortedProjects = context
        //        .Projects
        //        .OrderByDescending(x => x.StartDate)
        //        .Take(10)
        //        .Select(x => new
        //        {
        //            ProjectName = x.Name,
        //            Description = x.Description,
        //            StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
        //        })
        //        .OrderBy(x => x.ProjectName)
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var p in sortedProjects)
        //    {
        //        result.AppendLine(p.ProjectName);
        //        result.AppendLine(p.Description);
        //        result.AppendLine(p.StartDate);
        //    }

        //    return result.ToString().TrimEnd();
        //}

        //public static string IncreaseSalaries(SoftUniContext context)
        //{
        //    var sortedEmployees = context
        //        .Employees
        //        .Where(x => x.Department.Name == "Engineering"
        //                    || x.Department.Name == "Tool Design"
        //                    || x.Department.Name == "Marketing"
        //                    || x.Department.Name == "Information Services")
        //        .OrderBy(x => x.FirstName)
        //        .ThenBy(x => x.LastName)
        //        .ToList();

        //    foreach (var e in sortedEmployees)
        //    {
        //        e.Salary *= 1.12M;
        //    }

        //    context.SaveChanges();
        //    var result = new StringBuilder();

        //    foreach (var e in sortedEmployees)
        //    {
        //        result.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
        //    }

        //    return result.ToString().TrimEnd();
        //}

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sortedEmployees = context
                .Employees
                .Where(x => EF.Functions.Like(x.FirstName, "sa%"))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
            var result = new StringBuilder();

            foreach (var e in sortedEmployees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return result.ToString().TrimEnd();
        }
    }
}
