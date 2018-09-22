namespace BusTicketsSystem.Services
{
    using BusTicketsSystem.Models;
    using System.Collections.Generic;

    public interface ITicketService
    {
        Ticket Add(Customer customer, decimal price, int seat, Trip trip);

        IEnumerable<Ticket> TicketsByTripId(int tripId);
    }
}