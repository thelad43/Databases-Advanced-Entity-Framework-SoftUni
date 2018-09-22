namespace PhotoShare.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AlbumConfigurations : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .IsUnicode()
                   .HasMaxLength(50);

            builder.Property(e => e.BackgroundColor)
                   .IsRequired(false);

            builder.Property(e => e.IsPublic)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}