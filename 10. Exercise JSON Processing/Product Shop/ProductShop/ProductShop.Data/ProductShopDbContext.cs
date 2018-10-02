namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    using static Infrastructure.ProductShopDbConfiguration;

    public class ProductShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

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
                .Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder
                .Entity<CategoryProduct>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder
                .Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(c => c.ProductId);

            builder
                .Entity<Product>()
                .HasOne(p => p.Buyer)
                .WithMany(b => b.BoughtProducts)
                .HasForeignKey(p => p.BuyerId);

            builder
                .Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(s => s.SoldProducts)
                .HasForeignKey(p => p.SellerId);

            base.OnModelCreating(builder);
        }
    }
}