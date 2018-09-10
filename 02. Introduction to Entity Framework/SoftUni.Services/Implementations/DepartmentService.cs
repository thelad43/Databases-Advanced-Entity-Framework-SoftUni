namespace SoftUni.Services.Implementations
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class DepartmentService : IDepartmentService
    {
        private readonly SoftUniDbContext db;

        public DepartmentService(SoftUniDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DepartmentManagerWithEmployeesModel> DepartmentManagerWithEmployees()
        {
            var result = this.db
                .Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new DepartmentManagerWithEmployeesModel
                {
                    Name = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => new EmployeeNamesJobTitleModel
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            JobTitle = e.JobTitle
                        })
                        .ToList()
                })
                .ToList();

            return result;
        }
    }
}