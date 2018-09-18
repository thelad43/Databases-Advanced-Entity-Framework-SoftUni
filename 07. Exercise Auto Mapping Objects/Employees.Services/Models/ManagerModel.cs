namespace Employees.Services.Models
{
    using System.Collections.Generic;

    public class ManagerModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EmployeeInfoModel> Employees { get; set; }
    }
}