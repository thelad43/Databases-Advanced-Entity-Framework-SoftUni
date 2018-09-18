namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;
    using System.Text;

    public class ManagerInfoCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public ManagerInfoCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // managerinfo {Id}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);

            var manager = this.employees.ManagerInfo(id);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.Employees.Count}");

            foreach (var employee in manager.Employees)
            {
                stringBuilder.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");
            }

            return stringBuilder.ToString();
        }
    }
}