namespace BusTicketsSystem.App.Infrastructure
{
    using Models;
    using System;

    using static Common.ExceptionMessages;

    public static class Validator
    {
        public static void ThrowExceptionIfCustomerIsNull(Customer customer, int id)
        {
            if (customer == null)
            {
                throw new NullReferenceException(string.Format(CustomerDoesNotExistExceptionMessage, id));
            }
        }

        public static void ThrowExceptionIfTripIsNull(Trip trip, int id)
        {
            if (trip == null)
            {
                throw new NullReferenceException(string.Format(TripDoesNotExistExceptionMessage, id));
            }
        }

        public static void ThrowExceptionIfCompanyIsNull(Company company, string companyName)
        {
            if (company == null)
            {
                throw new NullReferenceException(string.Format(CompanyDoesNotExistExceptionMessage, companyName));
            }
        }
    }
}