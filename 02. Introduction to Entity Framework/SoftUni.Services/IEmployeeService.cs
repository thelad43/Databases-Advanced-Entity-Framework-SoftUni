namespace SoftUni.Services
{
    using Models;
    using SoftUni.Models;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        IEnumerable<Employee> ByLastName(string lastName);

        IEnumerable<EmployeeFullInfoModel> All();

        IEnumerable<string> EmployeesWithSalaryOver50000();

        IEnumerable<string> AllAddressesOrderedByDescAddressId();

        IEnumerable<EmployeeDepartmentModel> EmployeesFromResearchAndDevelopment();

        IEnumerable<EmployeesManagerProjectsModel> EmployeesManagerWithProjects();

        IEnumerable<AddressesByTownModel> AddressesByTown();

        EmployeeWithProjectsModel EmployeeWithProjects(int id);

        void IncreaseSalary(string[] departmentsNames, double percentages);

        IEnumerable<EmployeeNamesSalaryModel> EmployeesNamesSalary(string[] departmentsNames);

        IEnumerable<EmployeeShortInfoModel> EmployeesNamesStartingWith(string prefix);

        void ChangeAddress(Employee employee, Address newAddress);
    }
}