namespace BookShop.Initializer
{
    using Data;
    using Generators;
    using System;

    public class DbInitializer
    {
        public static void ResetDatabase(BookShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("BookShop database created successfully.");

            Seed(db);
        }

        private static void Seed(BookShopDbContext db)
        {
            var books = BookGenerator.CreateBooks();

            db.Books.AddRange(books);

            db.SaveChanges();

            Console.WriteLine("Sample data inserted successfully.");
        }
    }
}