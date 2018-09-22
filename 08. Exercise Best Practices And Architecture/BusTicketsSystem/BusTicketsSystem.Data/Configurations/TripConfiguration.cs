namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder
                .HasOne(e => e.OriginBusStation)
                .WithMany(s => s.DeparturesTrips)
                .HasForeignKey(e => e.OriginBusStationId);

            builder
                .HasOne(e => e.DestinationBusStation)
                .WithMany(s => s.ArrivingTrips)
                .HasForeignKey(e => e.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}