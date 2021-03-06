﻿namespace PhotoShare.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AlbumTagConfigurations : IEntityTypeConfiguration<AlbumTag>
    {
        public void Configure(EntityTypeBuilder<AlbumTag> builder)
        {
            builder.HasKey(e => new { e.AlbumId, e.TagId });

            builder.HasOne(e => e.Album)
                   .WithMany(a => a.AlbumTags)
                   .HasForeignKey(e => e.AlbumId);

            builder.HasOne(e => e.Tag)
                   .WithMany(a => a.AlbumTags)
                   .HasForeignKey(e => e.TagId);
        }
    }
}