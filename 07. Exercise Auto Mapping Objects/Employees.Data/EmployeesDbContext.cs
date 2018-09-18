namespace Employees.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    using static Infrastructure.Configurations.EmployeesDbConfiguration;

    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext()
        {
        }

        public EmployeesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

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
            builder
                .Entity<Employee>()
                .HasMany(m => m.Employees)
                .WithOne(e => e.Manager)
                .HasForeignKey(m => m.ManagerId);

            base.OnModelCreating(builder);
        }
    }
}