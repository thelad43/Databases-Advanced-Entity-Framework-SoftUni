namespace BusTicketsSystem.Services.Implementations
{
    using BusTicketsSystem.Models;
    using Data;
    using System.Linq;

    public class CompanyService : ICompanyService
    {
        private readonly BusTicketsSystemDbContext db;

        public CompanyService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public Company ByName(string name)
            => this.db
                .Companies
                .FirstOrDefault(c => c.Name == name);
    }
}