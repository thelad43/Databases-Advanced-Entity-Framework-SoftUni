namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;
    using System.Text;

    public class EmployeesOlderThanCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public EmployeesOlderThanCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // employeesolderthan {Age}

        public string Execute(params string[] arguments)
        {
            var age = int.Parse(arguments[1]);

            var employees = this.employees.EmployeesOlderThan(age);

            var stringBuilder = new StringBuilder();

            foreach (var employee in employees)
            {
                var manager = employee.Manager ?? "[no manager]";
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:F2} - Manager: {manager}");
            }

            return stringBuilder.ToString();
        }
    }
}