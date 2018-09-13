namespace Sales.Data
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;

    public class SalesDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Entity<Product>()
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValue("No Description");

            builder
               .Entity<Customer>()
               .Property(c => c.Name)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(100);

            builder
                .Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(80);

            builder
                .Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValue(DateTime.Now);

            builder
                .Entity<Store>()
                .Property(s => s.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(80);

            builder
                .Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder
                .Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

            builder
                .Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.StoreId);

            base.OnModelCreating(builder);
        }
    }
}