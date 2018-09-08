namespace Minions.Data
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MinionsDbContext : DbContext
    {
        public MinionsDbContext()
        {
        }

        public MinionsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Villain> Villains { get; set; }

        public DbSet<Minion> Minions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<EvilnessFactor> EvilnessFactors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(Configuration.ConnectionString);

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MinionsVillains>()
                .HasKey(mv => new { mv.MinionId, mv.VillainId });

            builder
                .Entity<MinionsVillains>()
                .HasOne(mv => mv.Minion)
                .WithMany(m => m.Villains)
                .HasForeignKey(v => v.MinionId);

            builder
                .Entity<MinionsVillains>()
                .HasOne(mv => mv.Villain)
                .WithMany(v => v.Minions)
                .HasForeignKey(m => m.VillainId);

            builder
                .Entity<Country>()
                .HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(t => t.CountryId);

            builder
                .Entity<Minion>()
                .HasOne(m => m.Town)
                .WithMany(t => t.Minions)
                .HasForeignKey(m => m.TownId);

            builder
                .Entity<Villain>()
                .HasOne(v => v.EvilnessFactor)
                .WithMany(ef => ef.Villains)
                .HasForeignKey(v => v.EvilnessFactorId);

            base.OnModelCreating(builder);
        }
    }
}