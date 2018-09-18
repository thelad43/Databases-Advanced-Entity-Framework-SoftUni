namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    public class EmployeePersonalInfoCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public EmployeePersonalInfoCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // employeepersonalinfo {Id}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);

            var employee = this.employees.EmployeePersonalInfo(id);

            return employee.ToString();
        }
    }
}