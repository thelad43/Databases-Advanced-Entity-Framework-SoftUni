namespace Employees.App.Core.Commands
{
    using Interfaces;
    using Services;

    using static Common.CommandMessages;

    public class SetBirthdayCommand : IExecutable
    {
        private readonly IEmployeeService employees;

        public SetBirthdayCommand(IEmployeeService employees)
        {
            this.employees = employees;
        }

        // setbirthday {Id} {Birthday}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);
            var birthday = arguments[2];

            this.employees.SetBirthday(id, birthday);

            return string.Format(SetBirthdayMessage, birthday, id);
        }
    }
}