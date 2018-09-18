namespace Employees.Data.Infrastructure
{
    using System;

    using static Common.ExceptionMessages;

    public static class Validator
    {
        public static void ThrowExceptionIfNullOrWhiteSpace(string text, string parameter)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(parameter, string.Format(NullOrWhiteSpaceExceptionMessage, parameter));
            }
        }

        public static void ThrowExceptionIfNegativeOrZero(int number, string parameter)
        {
            if (number <= 0)
            {
                throw new ArgumentException(string.Format(NegativeOrZeroExceptionMessage, parameter));
            }
        }
    }
}