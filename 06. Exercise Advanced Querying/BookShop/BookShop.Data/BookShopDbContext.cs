namespace BookShop.Data
{
    using Configurations;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BookShopDbContext : DbContext
    {
        private readonly Configuration configuration;

        public BookShopDbContext()
        {
            this.configuration = new Configuration();
        }

        public BookShopDbContext(DbContextOptions options)
            : base(options)
        {
            this.configuration = new Configuration();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(BookShopDbConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.configuration.Configure<Category, CategoryConfiguration>(builder);
            this.configuration.Configure<Book, BookConfiguration>(builder);
            this.configuration.Configure<Author, AuthorConfiguration>(builder);
            this.configuration.Configure<BookCategory, BookCategoryConfiguration>(builder);
        }
    }
}