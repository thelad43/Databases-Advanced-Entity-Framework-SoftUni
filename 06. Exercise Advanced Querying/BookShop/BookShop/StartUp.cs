namespace BookShop.App
{
    using Data;
    using Data.Infrastructure;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Implementations;
    using System;

    public class StartUp
    {
        private static IServiceProvider serviceProvider;

        private static IBookService books;
        private static IAuthorService authors;
        private static ICategoryService categories;

        public static void Main()
        {
            var db = new BookShopDbContext();
            DbInitializer.ResetDatabase(db);

            serviceProvider = ConfigureServices();

            books = serviceProvider.GetService<IBookService>();
            authors = serviceProvider.GetService<IAuthorService>();
            categories = serviceProvider.GetService<ICategoryService>();

            // Problem 01. Age Restriction
            // Problem01();

            // Problem 02. Golden Books
            // Problem02();

            // Problem 03. Books by Price
            // Problem03();

            // Problem 04. Not Released In
            // Problem04();

            // Problem 05. Book Titles by Category
            // Problem05();

            // Problem 06. Released Before Date
            // Problem06();

            // Problem 07. Author Search
            // Problem07();

            // Problem 08. Book Search
            // Problem08();

            // Problem 09. Book Search by Author
            // Problem09();

            // Problem 10. Count Books
            // Problem10();

            // Problem 11. Total Book Copies
            // Problem11();

            // Problem 12. Profit by Category
            // Problem12();

            // Problem 13. Most Recent Books
            // Problem13();

            // Problem 14. Increase Prices
            // books.IncreasePrices();

            // Problem 15. Remove Books
            // books.RemoveBooks();
        }

        private static void Problem01()
        {
            var ageRestriction = Console.ReadLine();

            var titles = books.GetBooksByAgeRestriction(ageRestriction);

            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void Problem02()
        {
            var titles = books.GetGoldenBooks();

            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void Problem03()
        {
            var result = books.GetBooksByPrice();

            foreach (var titlePrice in result)
            {
                Console.WriteLine($"{titlePrice.Title} - ${titlePrice.Price:F2}");
            }
        }

        private static void Problem04()
        {
            var year = int.Parse(Console.ReadLine());

            var titles = books.GetBooksNotRealeasedIn(year);

            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void Problem05()
        {
            var categories = Console.ReadLine(); // "horror mystery drama"

            var titles = books.GetBooksByCategory(categories);

            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void Problem06()
        {
            var date = Console.ReadLine(); // "12-04-1992"

            var result = books.GetBooksReleasedBefore(date);

            foreach (var book in result)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType} - ${book.Price}");
            }
        }

        private static void Problem07()
        {
            var search = Console.ReadLine();

            var names = authors.GetAuthorNamesEndingIn(search);

            Console.WriteLine(string.Join(Environment.NewLine, names));
        }

        private static void Problem08()
        {
            var search = Console.ReadLine();

            var titles = books.GetBookTitlesContaining(search);

            Console.WriteLine(string.Join(Environment.NewLine, titles));
        }

        private static void Problem09()
        {
            var search = Console.ReadLine();

            var result = books.GetBooksByAuthor(search);

            foreach (var titleAuthor in result)
            {
                Console.WriteLine($"{titleAuthor.Title} ({titleAuthor.Name})");
            }
        }

        private static void Problem10()
        {
            var length = int.Parse(Console.ReadLine());

            Console.WriteLine(books.CountBooks(length));
        }

        private static void Problem11()
        {
            var result = books.CountCopiesByAuthor();

            foreach (var authorCopies in result)
            {
                Console.WriteLine($"{authorCopies.Name} - {authorCopies.Copies}");
            }
        }

        private static void Problem12()
        {
            var result = categories.GetTotalProfitByCategory();

            foreach (var categoryProfit in result)
            {
                Console.WriteLine($"{categoryProfit.Category} ${categoryProfit.Profit}");
            }
        }

        private static void Problem13()
        {
            var result = categories.GetMostRecentBooks();

            foreach (var categoryBook in result)
            {
                Console.WriteLine($"--{categoryBook.Category}");

                foreach (var book in categoryBook.Books)
                {
                    Console.WriteLine($"{book.Title} ({book.Year})");
                }
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BookShopDbContext>(options =>
                options.UseSqlServer(BookShopDbConfiguration.ConnectionString));

            services.AddTransient<IBookService, BookService>();

            services.AddTransient<IAuthorService, AuthorService>();

            services.AddTransient<ICategoryService, CategoryService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}