﻿namespace BookShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(e => new { e.BookId, e.CategoryId });

            builder.HasOne(e => e.Category)
                .WithMany(c => c.CategoryBooks)
                .HasForeignKey(e => e.CategoryId);

            builder.HasOne(e => e.Book)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(e => e.BookId);
        }
    }
}