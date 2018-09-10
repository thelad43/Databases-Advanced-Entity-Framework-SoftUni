namespace SoftUni.Services.Implementations
{
    using Data;
    using System;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly SoftUniDbContext db;

        public TownService(SoftUniDbContext db)
        {
            this.db = db;
        }

        public int Delete(string name)
        {
            var town = this.db
                .Towns
                .Where(t => t.Name == name)
                .FirstOrDefault();

            if (town == null)
            {
                throw new NullReferenceException();
            }

            var addresses = this.db.Addresses.Where(a => a.TownId == town.TownId).ToList();

            var employees = this.db.Employees.Where(e => e.Address.Town == town).ToList();

            var deletedAddresses = 0;

            foreach (var employee in employees)
            {
                employee.AddressId = null;
                deletedAddresses++;
            }

            this.db.RemoveRange(addresses);

            this.db.Remove(town);

            this.db.SaveChanges();

            return deletedAddresses;
        }
    }
}