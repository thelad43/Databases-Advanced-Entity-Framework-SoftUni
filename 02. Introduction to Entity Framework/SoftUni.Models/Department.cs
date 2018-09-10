namespace SoftUni.Models
{
    using System.Collections.Generic;

    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int ManagerId { get; set; }

        public Employee Manager { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}