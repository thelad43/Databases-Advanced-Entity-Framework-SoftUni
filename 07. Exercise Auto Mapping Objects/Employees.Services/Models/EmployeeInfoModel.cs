namespace Employees.Services.Models
{
    public class EmployeeInfoModel : EmployeeBasicInfoModel
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id}, Name: {this.FirstName} {this.LastName}, Salary: ${this.Salary:F2}";
        }
    }
}