namespace Employees.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Infrastructure;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using static Common.ExceptionMessages;
    using static Common.GlobalConstants;

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesDbContext db;

        public EmployeeService(EmployeesDbContext db)
        {
            this.db = db;
        }

        public void Add(string firstName, string lastName, decimal salary)
        {
            Validator.ThrowExceptionIfNullOrWhiteSpace(firstName, "First Name");
            Validator.ThrowExceptionIfNullOrWhiteSpace(lastName, "Last Name");
            Validator.ThrowExceptionIfNegativeOrZero((int)salary, "Salary");

            this.db.Add(new Employee(firstName, lastName, salary));
            this.db.SaveChanges();
        }

        public void SetBirthday(int id, string birthday)
        {
            Validator.ThrowExceptionIfNegativeOrZero(id, nameof(id));
            Validator.ThrowExceptionIfNullOrWhiteSpace(birthday, "Birthdate");

            var employee = this.db
                .Employees
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), string.Format(NotFoundEmployeeExceptionMessage, id));
            }

            employee.Birthdate = DateTime.ParseExact(birthday, DateFormat, CultureInfo.InvariantCulture);
            this.db.SaveChanges();
        }

        public void SetAddress(int id, string address)
        {
            Validator.ThrowExceptionIfNegativeOrZero(id, "ID");
            Validator.ThrowExceptionIfNullOrWhiteSpace(address, "Address");

            var employee = this.db
               .Employees
               .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), string.Format(NotFoundEmployeeExceptionMessage, id));
            }

            employee.Address = address;
            this.db.SaveChanges();
        }

        public EmployeeInfoModel EmployeeInfo(int id)
        {
            Validator.ThrowExceptionIfNegativeOrZero(id, "ID");

            var employee = this.db
                .Employees
                .Where(e => e.Id == id)
                .ProjectTo<EmployeeInfoModel>()
                .FirstOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), string.Format(NotFoundEmployeeExceptionMessage, id));
            }

            return employee;
        }

        public Employee EmployeePersonalInfo(int id)
        {
            Validator.ThrowExceptionIfNegativeOrZero(id, "ID");

            var employee = this.db
                .Employees
                .Where(e => e.Id == id)
                .ProjectTo<Employee>()
                .FirstOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), string.Format(NotFoundEmployeeExceptionMessage, id));
            }

            return employee;
        }

        public void SetManager(int employeeId, int managerId)
        {
            Validator.ThrowExceptionIfNegativeOrZero(employeeId, "Employee ID");
            Validator.ThrowExceptionIfNegativeOrZero(managerId, "Manager ID");

            var employee = this.db
                .Employees
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), string.Format(NotFoundEmployeeExceptionMessage, employeeId));
            }

            var manager = this.db
                .Employees
                .FirstOrDefault(e => e.Id == managerId);

            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), string.Format(NotFoundManagerExceptionMessage, managerId));
            }

            if (employeeId == managerId)
            {
                throw new InvalidOperationException(EmployeeCannotBeManagerToHimselfExceptionMessage);
            }

            employee.ManagerId = manager.Id;
            employee.Manager = manager;

            this.db.SaveChanges();
        }

        public ManagerModel ManagerInfo(int id)
        {
            Validator.ThrowExceptionIfNegativeOrZero(id, "Manager ID");

            var manager = this.db
                .Employees
                .Where(e => e.Id == id)
                .ProjectTo<ManagerModel>()
                .FirstOrDefault();

            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), string.Format(NotFoundManagerExceptionMessage, id));
            }

            return manager;
        }

        public IEnumerable<EmployeeManagerModel> EmployeesOlderThan(int age)
            => this.db
                .Employees
                .Where(e => DateTime.Now.Year - e.Birthdate.Year > age)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeManagerModel>()
                .ToList();
    }
}