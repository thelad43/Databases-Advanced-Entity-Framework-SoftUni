namespace SoftUni.Services.Implementations
{
    using Data;
    using Models;
    using SoftUni.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        private readonly SoftUniDbContext db;

        public EmployeeService(SoftUniDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EmployeeFullInfoModel> All()
        {
            var employees = this.db
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new EmployeeFullInfoModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    MiddleName = e.MiddleName,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary
                })
                .ToList();

            return employees;
        }

        public IEnumerable<string> EmployeesWithSalaryOver50000()
        {
            var employees = this.db
                .Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => e.FirstName)
                .ToList();

            return employees;
        }

        public IEnumerable<EmployeeDepartmentModel> EmployeesFromResearchAndDevelopment()
        {
            var employees = this.db
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new EmployeeDepartmentModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DepartmentName = e.Department.Name,
                    Salary = e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> ByLastName(string lastName)
        {
            var employees = this.db
                .Employees
                .Where(e => e.LastName == lastName)
                .ToList();

            return employees;
        }

        public IEnumerable<string> AllAddressesOrderedByDescAddressId()
        {
            var addresses = this.db
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            return addresses;
        }

        public IEnumerable<EmployeesManagerProjectsModel> EmployeesManagerWithProjects()
        {
            var employees = this.db
                .Employees
                .Where(e => e.EmployeesProjects
                .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(30)
                .Select(e => new EmployeesManagerProjectsModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new ProjectNameStartEndDateModel
                    {
                        Name = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    })
                })
                .ToList();

            return employees;
        }

        public IEnumerable<AddressesByTownModel> AddressesByTown()
        {
            var addresses = this.db
                .Addresses
                .Select(a => new AddressesByTownModel
                {
                    AddressText = a.AddressText,
                    TownName = a.Town.Name,
                    Employees = a.Employees.Count
                })
                .OrderByDescending(a => a.Employees)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            return addresses;
        }

        public EmployeeWithProjectsModel EmployeeWithProjects(int id)
        {
            var employee = this.db
                .Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeWithProjectsModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    Projects = e.EmployeesProjects
                        .OrderBy(p => p.Project.Name)
                        .Select(ep => ep.Project.Name)
                })
                .FirstOrDefault();

            if (employee == null)
            {
                throw new NullReferenceException();
            }

            return employee;
        }

        public void IncreaseSalary(string[] departmentsNames, double percentages)
        {
            this.db
                .Employees
                .Where(e => departmentsNames.Contains(e.Department.Name))
                .ToList()
                .ForEach(e => e.Salary *= (decimal)(percentages / 100 + 1));

            this.db.SaveChanges();
        }

        public IEnumerable<EmployeeNamesSalaryModel> EmployeesNamesSalary(string[] departmentsNames)
        {
            var employees = this.db
                .Employees
                .Where(e => departmentsNames.Contains(e.Department.Name))
                .Select(e => new EmployeeNamesSalaryModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            return employees;
        }

        public IEnumerable<EmployeeShortInfoModel> EmployeesNamesStartingWith(string prefix)
        {
            var employees = this.db
                .Employees
                .Where(e => e.FirstName.StartsWith(prefix))
                .Select(e => new EmployeeShortInfoModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            return employees;
        }

        public void ChangeAddress(Employee employee, Address newAddress)
        {
            employee.Address = newAddress;

            this.db.SaveChanges();
        }
    }
}