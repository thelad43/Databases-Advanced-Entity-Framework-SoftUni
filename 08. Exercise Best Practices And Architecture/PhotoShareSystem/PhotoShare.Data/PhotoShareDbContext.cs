namespace PhotoShare.Data
{
    using Configurations;
    using Microsoft.EntityFrameworkCore;
    using Models;

    using static Configurations.PhotoShareDbConfiguration;

    public class PhotoShareDbContext : DbContext
    {
        private readonly Configuration configuration;

        public PhotoShareDbContext()
        {
            this.configuration = new Configuration();
        }

        public PhotoShareDbContext(DbContextOptions options)
            : base(options)
        {
            this.configuration = new Configuration();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<AlbumRole> AlbumRoles { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<AlbumTag> AlbumTags { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

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
            this.configuration.Configure<Album, AlbumConfigurations>(builder);
            this.configuration.Configure<AlbumRole, AlbumRoleConfigurations>(builder);
            this.configuration.Configure<AlbumTag, AlbumTagConfigurations>(builder);
            this.configuration.Configure<Friendship, FriendshipConfigurations>(builder);
            this.configuration.Configure<Picture, PictureConfigurations>(builder);
            this.configuration.Configure<Tag, TagConfigurations>(builder);
            this.configuration.Configure<Town, TownConfigurations>(builder);
            this.configuration.Configure<User, UserConfigurations>(builder);
        }
    }
}