namespace SoftUni.Services.Models
{
    using System.Collections.Generic;

    public class EmployeesWithProjectsModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<ProjectNameStartEndDateModel> Projects { get; set; }
    }
}