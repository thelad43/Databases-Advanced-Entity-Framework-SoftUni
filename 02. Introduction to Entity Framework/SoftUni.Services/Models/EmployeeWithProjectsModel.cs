namespace SoftUni.Services.Models
{
    using System.Collections.Generic;

    public class EmployeeWithProjectsModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public IEnumerable<string> Projects { get; set; }
    }
}