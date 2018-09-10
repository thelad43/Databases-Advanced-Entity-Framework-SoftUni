namespace SoftUni.App
{
    using Data;
    using Data.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Services;
    using Services.Implementations;
    using System;
    using System.Linq;

    public class StartUp
    {
        private const string DateFormat = "M/d/yyyy h:mm:ss tt";

        private static IServiceProvider serviceProvider;

        public static void Main()
        {
            serviceProvider = ConfigureServices();

            // Problem 03. Employees Full Information
            // Problem03();

            // Problem 04. Employees with Salary Over 50 000
            // var employees = employeeService.EmployeesWithSalaryOver50000();
            // Console.WriteLine(string.Join(Environment.NewLine, employees));

            // Problem 05. Employees from Research and Development
            // Problem05();

            // Problem 06. Adding a New Address and Updating Employee
            // Problem06();

            // Problem 07. Employees and Projects
            // Problem07();

            // Problem 08. Addresses by Town
            // Problem08();

            // Problem 09. Employee 147
            // Problem09();

            // Problem 10. Departments with More Than 5 Employees
            // Problem10();

            // Problem 11. Find Latest 10 Projects
            // Problem11();

            // Problem 12. Increase Salaries
            // Problem12();

            // Problem 13. Find Employees by First Name Starting With "Sa"
            // Problem13();

            // Problem 14. Delete Project by Id
            //Problem14();

            // Problem 15. Remove Towns
            // Problem15();
        }

        private static void Problem03()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var employees = employeeService.All();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.MiddleName} {e.LastName} {e.JobTitle} {e.Salary:F2}");
            }
        }

        private static void Problem05()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var employees = employeeService.EmployeesFromResearchAndDevelopment();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
            }
        }

        private static void Problem06()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var address = new Address { AddressText = "Vitoshka 15", TownId = 4 };
            var lastName = "Nakov";
            var employee = employeeService.ByLastName(lastName).FirstOrDefault();

            employeeService.ChangeAddress(employee, address);

            var addresses = employeeService.AllAddressesOrderedByDescAddressId();

            Console.WriteLine(string.Join(Environment.NewLine, addresses));
        }

        private static void Problem07()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var employees = employeeService.EmployeesManagerWithProjects();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} – Manager: {e.ManagerFirstName} {e.ManagerLastName}");

                foreach (var p in e.Projects)
                {
                    Console.WriteLine($"--{p.Name} - {p.StartDate.ToString(DateFormat)} - {p.EndDate?.ToString(DateFormat) ?? "not finished" }");
                }

                Console.WriteLine();
            }
        }

        private static void Problem08()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var addresses = employeeService.AddressesByTown();

            foreach (var address in addresses)
            {
                Console.WriteLine($"{address.AddressText}, {address.TownName} - {address.Employees} employees");
            }
        }

        private static void Problem09()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var id = 147;
            var employee = employeeService.EmployeeWithProjects(id);

            Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var projectName in employee.Projects)
            {
                Console.WriteLine(projectName);
            }
        }

        private static void Problem10()
        {
            var departmentService = serviceProvider.GetService<IDepartmentService>();

            var departments = departmentService.DepartmentManagerWithEmployees();

            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Name} – {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }

                Console.WriteLine(new string('-', 10));
            }
        }

        private static void Problem11()
        {
            var projectService = serviceProvider.GetService<IProjectService>();

            var projects = projectService.Last10StartedProjects();

            foreach (var project in projects)
            {
                Console.WriteLine(project.Name);
                Console.WriteLine(project.Description);
                Console.WriteLine(project.StartDate.ToString(DateFormat));
            }
        }

        private static void Problem12()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var departments = new[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

            employeeService.IncreaseSalary(departments, 12);

            var employees = employeeService.EmployeesNamesSalary(departments);

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }
        }

        private static void Problem13()
        {
            var employeeService = serviceProvider.GetService<IEmployeeService>();

            var employees = employeeService.EmployeesNamesStartingWith("Sa");

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} (${e.Salary})");
            }
        }

        private static void Problem14()
        {
            var projectService = serviceProvider.GetService<IProjectService>();

            var id = 2;

            projectService.Delete(id);

            var projectsNames = projectService.TakeProjectNames(10);

            Console.WriteLine(string.Join(Environment.NewLine, projectsNames));
        }

        private static void Problem15()
        {
            var townService = serviceProvider.GetService<ITownService>();

            var townName = Console.ReadLine();
            var deletedAddresses = townService.Delete(townName);
            var addressWord = deletedAddresses == 1 ? "address" : "addresses";
            Console.WriteLine($"{deletedAddresses} {addressWord} in {townName} were deleted");
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SoftUniDbContext>(options =>
                options.UseSqlServer(Configuration.ConnectionString));

            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddTransient<IProjectService, ProjectService>();

            services.AddTransient<IDepartmentService, DepartmentService>();

            services.AddTransient<ITownService, TownService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}