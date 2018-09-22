namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder
                .HasMany(t => t.BusStations)
                .WithOne(s => s.Town)
                .HasForeignKey(t => t.TownId);
        }
    }
}