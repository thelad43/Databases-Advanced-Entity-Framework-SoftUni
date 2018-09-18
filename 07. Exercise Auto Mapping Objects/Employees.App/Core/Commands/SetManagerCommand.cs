namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    using static Common.CommandMessages;

    public class SetManagerCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public SetManagerCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // setmanager {EmployeeId} {ManagerId}
        public string Execute(params string[] arguments)
        {
            var employeeId = int.Parse(arguments[1]);
            var managerId = int.Parse(arguments[2]);

            this.employees.SetManager(employeeId, managerId);

            return string.Format(SetManagerMessage, employeeId, managerId);
        }
    }
}