namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    using static Common.CommandMessages;

    public class SetAddressCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public SetAddressCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // setaddress {Id} {Address}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);
            var address = arguments[2];

            this.employees.SetAddress(id, address);

            return string.Format(SetAddressMessage, address, id);
        }
    }
}