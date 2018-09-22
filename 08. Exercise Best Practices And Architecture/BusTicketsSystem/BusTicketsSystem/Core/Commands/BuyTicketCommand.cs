namespace BusTicketsSystem.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using Models.Enums;
    using Services;
    using System;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class BuyTicketCommand : ICommand
    {
        private readonly ICustomerService customers;
        private readonly ITripService trips;
        private readonly IBankAccountService bankAccounts;
        private readonly ITicketService tickets;

        public BuyTicketCommand(
            ICustomerService customers,
            ITripService trips,
            IBankAccountService bankAccount,
            ITicketService tickets)
        {
            this.customers = customers;
            this.trips = trips;
            this.bankAccounts = bankAccount;
            this.tickets = tickets;
        }

        // BuyTicket {CustomerId} {TripId} {Price} {Seat}
        public string Execute(params string[] arguments)
        {
            var customerId = int.Parse(arguments[1]);
            var tripId = int.Parse(arguments[2]);
            var price = decimal.Parse(arguments[3]);
            var seat = int.Parse(arguments[4]);

            var customer = this.customers.ById(customerId);
            var trip = this.trips.ById(tripId);

            Validator.ThrowExceptionIfCustomerIsNull(customer, customerId);
            Validator.ThrowExceptionIfTripIsNull(trip, tripId);

            if (trip.Status == Status.Arrived)
            {
                throw new InvalidOperationException(string.Format(TripHasAlreadyArrivedExceptionMessage, tripId));
            }

            if (trip.Status == Status.Cancelled)
            {
                throw new InvalidOperationException(string.Format(TripCancelledExceptionMessage, tripId));
            }

            this.bankAccounts.Withdraw(customer.BankAccountId, price);

            this.tickets.Add(customer, price, seat, trip);

            return string.Format(BuyTicketSuccessMessage, customer.FirstName, customer.LastName, tripId, price, seat);
        }
    }
}