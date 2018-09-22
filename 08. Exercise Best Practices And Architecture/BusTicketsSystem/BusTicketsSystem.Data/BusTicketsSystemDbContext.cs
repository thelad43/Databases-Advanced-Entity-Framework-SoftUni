namespace BusTicketsSystem.Data
{
    using Data.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Models;

    using static Configurations.BusTicketsSystemDbConfiguration;

    public class BusTicketsSystemDbContext : DbContext
    {
        private readonly Configuration configuration;

        public BusTicketsSystemDbContext()
        {
            this.configuration = new Configuration();
        }

        public BusTicketsSystemDbContext(DbContextOptions options)
            : base(options)
        {
            this.configuration = new Configuration();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BusStation> BusStations { get; set; }

        public DbSet<ArrivedTrip> ArrivedTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.configuration.Configure<Customer, CustomerConfiguration>(builder);
            this.configuration.Configure<Company, CompanyConfiguration>(builder);
            this.configuration.Configure<Town, TownConfiguration>(builder);
            this.configuration.Configure<Trip, TripConfiguration>(builder);

            base.OnModelCreating(builder);
        }
    }
}