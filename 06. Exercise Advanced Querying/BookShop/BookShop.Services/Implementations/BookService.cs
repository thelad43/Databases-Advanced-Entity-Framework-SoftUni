namespace BookShop.Services.Implementations
{
    using BookShop.Models;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using static Common.GlobalConstants;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<string> GetBooksByAgeRestriction(string ageRestrictionAsString)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;

            ageRestrictionAsString = textInfo.ToTitleCase(ageRestrictionAsString);

            var ageRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), ageRestrictionAsString);

            var titles = this.db
                .Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            return titles;
        }

        public IEnumerable<string> GetGoldenBooks()
        {
            var titles = this.db
                .Books
                .Where(b => b.EditionType == EditionType.Gold)
                .Where(b => b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return titles;
        }

        public IEnumerable<TitlePriceModel> GetBooksByPrice()
        {
            var titles = this.db
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new TitlePriceModel
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .ToList();

            return titles;
        }

        public IEnumerable<string> GetBooksNotRealeasedIn(int year)
        {
            var titles = this.db
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return titles;
        }

        public IEnumerable<string> GetBooksByCategory(string categoriesAsString)
        {
            categoriesAsString = categoriesAsString.ToLower();

            var categories = categoriesAsString
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var titles = this.db
                .Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name)))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return titles;
        }

        public IEnumerable<TitleEditionTypePriceModel> GetBooksReleasedBefore(string dateAsString)
        {
            var date = DateTime.ParseExact(dateAsString, DateFormat, CultureInfo.InvariantCulture);

            var books = this.db
                .Books
                .Where(b => b.ReleaseDate < date)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new TitleEditionTypePriceModel
                {
                    Title = b.Title,
                    EditionType = b.EditionType,
                    Price = b.Price
                })
                .ToList();

            return books;
        }

        public IEnumerable<string> GetBookTitlesContaining(string search)
        {
            search = search.ToLower();

            var titles = this.db
                .Books
                .Where(b => b.Title.ToLower().Contains(search))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return titles;
        }

        public IEnumerable<TitleAuthorNameModel> GetBooksByAuthor(string search)
        {
            search = search.ToLower();

            var result = this.db
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(search))
                .OrderBy(b => b.BookId)
                .Select(b => new TitleAuthorNameModel
                {
                    Title = b.Title,
                    Name = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToList();

            return result;
        }

        public int CountBooks(int length)
            => this.db
                .Books
                .Where(b => b.Title.Length > length)
                .Count();

        public IEnumerable<AuthorBookCopiesModel> CountCopiesByAuthor()
            => this.db
                .Books
                .Select(b => new AuthorBookCopiesModel
                {
                    Name = $"{b.Author.FirstName} {b.Author.LastName}",
                    Copies = b.Copies
                })
                .OrderByDescending(b => b.Copies)
                .ToList();

        public void IncreasePrices()
        {
            var books = this.db
              .Books
              .Where(b => b.ReleaseDate.Value.Year < Year)
              .ToList();

            foreach (var book in books)
            {
                book.Price += IncreasingPrice;
            }

            this.db.SaveChanges();
        }

        public int RemoveBooks()
        {
            var books = this.db
                .Books
                .Where(b => b.Copies < MinimumCopies)
                .ToList();

            var count = books.Count;

            this.db.RemoveRange(books);
            this.db.SaveChanges();

            return count;
        }
    }
}