namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasMany(c => c.Trips)
                .WithOne(t => t.Company)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Company)
                .HasForeignKey(c => c.CompanyId);
        }
    }
}