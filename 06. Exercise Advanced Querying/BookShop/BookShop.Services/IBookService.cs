namespace BookShop.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<string> GetBooksByAgeRestriction(string ageRestriction);

        IEnumerable<string> GetGoldenBooks();

        IEnumerable<TitlePriceModel> GetBooksByPrice();

        IEnumerable<string> GetBooksNotRealeasedIn(int year);

        IEnumerable<string> GetBooksByCategory(string categoriesAsString);

        IEnumerable<TitleEditionTypePriceModel> GetBooksReleasedBefore(string dateAsString);

        IEnumerable<string> GetBookTitlesContaining(string search);

        IEnumerable<TitleAuthorNameModel> GetBooksByAuthor(string search);

        int CountBooks(int length);

        IEnumerable<AuthorBookCopiesModel> CountCopiesByAuthor();

        void IncreasePrices();

        int RemoveBooks();
    }
}