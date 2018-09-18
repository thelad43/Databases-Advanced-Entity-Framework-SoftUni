namespace Employees.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using static Common.GlobalConstants;

    public class Employee
    {
        public Employee(string firstName, string lastName, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime Birthdate { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"ID: {this.Id} - {this.FirstName} {this.LastName} - ${this.Salary:F2}");
            stringBuilder.AppendLine($"Birthday: {this.Birthdate.ToString(DateFormat)}");
            stringBuilder.AppendLine($"Address: {this.Address}");

            return stringBuilder.ToString();
        }
    }
}