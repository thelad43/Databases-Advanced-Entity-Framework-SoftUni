namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
             .HasOne(c => c.HomeTown)
             .WithMany(t => t.Customers)
             .HasForeignKey(c => c.HomeTownId);

            builder
                .HasMany(c => c.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(c => c.CustomerId);

            builder
                .HasOne(c => c.BankAccount)
                .WithOne(a => a.Customer)
                .HasForeignKey<BankAccount>(c => c.CustomerId);
        }
    }
}