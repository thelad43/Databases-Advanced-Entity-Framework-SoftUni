namespace BusTicketsSystem.Services.Implementations
{
    using BusTicketsSystem.Models;
    using Data;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly BusTicketsSystemDbContext db;

        public CustomerService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public Customer ById(int id)
            => this.db
                .Customers
                .FirstOrDefault(c => c.Id == id);
    }
}