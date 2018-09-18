namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    using static Common.CommandMessages;

    public class AddEmployeeCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public AddEmployeeCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // addemployee {FirstName} {LastName} {Salary}
        public string Execute(params string[] arguments)
        {
            var firstName = arguments[1];
            var lastName = arguments[2];
            var salary = decimal.Parse(arguments[3]);

            this.employees.Add(firstName, lastName, salary);

            return string.Format(AddEmployeeMessage, firstName);
        }
    }
}