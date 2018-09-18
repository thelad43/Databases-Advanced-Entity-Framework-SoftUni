namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    public class EmployeeInfoCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public EmployeeInfoCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // employeeinfo {Id}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);

            var employee = this.employees.EmployeeInfo(id);

            return employee.ToString();
        }
    }
}