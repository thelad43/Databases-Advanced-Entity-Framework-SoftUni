namespace BusTicketsSystem.Common
{
    public static class ExceptionMessages
    {
        public const string CustomerDoesNotExistExceptionMessage = "Customer with id: {0} does not exist!";
        public const string TripDoesNotExistExceptionMessage = "Trip with id: {0} does not exist!";
        public const string TripHasAlreadyArrivedExceptionMessage = "Trip with id: {0} is already arrived!";
        public const string TripCancelledExceptionMessage = "Trip with id: {0} is cancelled!";
        public const string CompanyDoesNotExistExceptionMessage = "Company {0} does not exist!";
        public const string StatusIsAlreadySetExceptionMessage = "Status is already set to {0}!";
        public const string TripIsAlreadyArrivedExceptionMessage = "Trip already finished. Arrived!";
    }
}