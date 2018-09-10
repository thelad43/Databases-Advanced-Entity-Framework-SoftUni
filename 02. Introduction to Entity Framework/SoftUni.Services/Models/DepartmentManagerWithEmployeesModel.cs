namespace SoftUni.Services.Models
{
    using System.Collections.Generic;

    public class DepartmentManagerWithEmployeesModel
    {
        public string Name { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public IEnumerable<EmployeeNamesJobTitleModel> Employees { get; set; }
    }
}