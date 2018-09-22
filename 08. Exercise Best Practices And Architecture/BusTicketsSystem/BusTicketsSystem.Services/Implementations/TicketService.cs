namespace BusTicketsSystem.Services.Implementations
{
    using BusTicketsSystem.Models;
    using Data;
    using System.Collections.Generic;
    using System.Linq;

    public class TicketService : ITicketService
    {
        private readonly BusTicketsSystemDbContext db;

        public TicketService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public Ticket Add(Customer customer, decimal price, int seat, Trip trip)
        {
            var ticket = new Ticket
            {
                Customer = customer,
                CustomerId = customer.Id,
                Price = price,
                Seat = seat,
                Trip = trip,
                TripId = trip.Id,
            };

            this.db.Add(ticket);
            this.db.SaveChanges();

            return ticket;
        }

        public IEnumerable<Ticket> TicketsByTripId(int tripId)
            => this.db
                .Tickets
                .Where(t => t.TripId == tripId)
                .ToList();
    }
}