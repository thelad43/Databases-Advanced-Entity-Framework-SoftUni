namespace BusTicketsSystem.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly BusTicketsSystemDbContext db;

        public DatabaseInitializerService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public void InitializeDatabase()
        {
            this.db.Database.Migrate();
        }
    }
}