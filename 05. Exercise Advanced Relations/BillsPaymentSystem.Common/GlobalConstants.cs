namespace BillsPaymentSystem.Common
{
    public static class GlobalConstants
    {
        public const int MaxLengthName = 50;

        public const int MaxLengthEmail = 80;

        public const int MaxLengthPassword = 25;

        public const int MaxLengthSwiftCode = 20;

        public const int UsersCount = 13;

        public const string NegativeWithdrawExceptionMessage = "Cannot withdraw negative amount of money!";

        public const string InsufficientFundsExceptionMessage = "Insufficient funds!";

        public const string InsufficientLimitExceptionMessage = "Insufficient limit!";

        public const string NegativeDepositExceptionMessage = "Cannot deposit negative amount of money!";

        public const string DepositTooMuchExceptionMessage = "The deposit is bigger than the owed sum!";

        public const string CannotAffordPaymentExceptionMessage = "User cannot afford this payment";

        public const string AmountGreaterThanPossibilities = "Amount is greater than the cards possibilities";
    }
}