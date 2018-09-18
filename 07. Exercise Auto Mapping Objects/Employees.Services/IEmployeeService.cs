namespace Employees.Services
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        void Add(string firstName, string lastName, decimal salary);

        void SetBirthday(int id, string birthday);

        void SetAddress(int id, string address);

        EmployeeInfoModel EmployeeInfo(int id);

        Employee EmployeePersonalInfo(int id);

        void SetManager(int employeeId, int managerId);

        ManagerModel ManagerInfo(int id);

        IEnumerable<EmployeeManagerModel> EmployeesOlderThan(int age);
    }
}