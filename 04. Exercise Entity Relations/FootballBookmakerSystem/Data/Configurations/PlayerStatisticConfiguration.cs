namespace FootballBookmakerSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder
                .HasKey(ps => new { ps.GameId, ps.PlayerId });

            builder
                .HasOne(ps => ps.Game)
                .WithMany(g => g.Players)
                .HasForeignKey(p => p.GameId);

            builder
                .HasOne(ps => ps.Player)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}