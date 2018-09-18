namespace Employees.Common
{
    public static class ExceptionMessages
    {
        public const string NullOrWhiteSpaceExceptionMessage = "{0} cannot be null or white space!";
        public const string NegativeOrZeroExceptionMessage = "{0} cannot be zero or negative number!";
        public const string NotFoundEmployeeExceptionMessage = "Employee with id: {0} is not existing!";
        public const string NotFoundManagerExceptionMessage = "Manager with id: {0} is not existing!";
        public const string EmployeeCannotBeManagerToHimselfExceptionMessage = "Employee cannot be manager to himself!";
    }
}