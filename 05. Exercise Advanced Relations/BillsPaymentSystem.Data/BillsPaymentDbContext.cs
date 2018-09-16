namespace BillsPaymentSystem.Data
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BillsPaymentDbContext : DbContext
    {
        public BillsPaymentDbContext()
        {
        }

        public BillsPaymentDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(BillsPaymentDbConfiguration.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<PaymentMethod>()
                .HasIndex(m => new { m.UserId, m.BankAccountId, m.CreditCardId })
                .IsUnique();

            builder
                .Entity<User>()
                .HasMany(u => u.PaymentMethods)
                .WithOne(m => m.User)
                .HasForeignKey(u => u.UserId);

            builder
                .Entity<PaymentMethod>()
                .HasOne(m => m.CreditCard)
                .WithOne(c => c.PaymentMethod)
                .HasForeignKey<PaymentMethod>(m => m.CreditCardId);

            builder
                .Entity<PaymentMethod>()
                .HasOne(m => m.BankAccount)
                .WithOne(a => a.PaymentMethod)
                .HasForeignKey<PaymentMethod>(m => m.BankAccountId);

            base.OnModelCreating(builder);
        }
    }
}