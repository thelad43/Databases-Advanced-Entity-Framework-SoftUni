namespace BookShop.Services.Implementations
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryProfitModel> GetTotalProfitByCategory()
            => this.db
                .Categories
                .Select(c => new CategoryProfitModel
                {
                    Category = c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Price * cb.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Category)
                .ToList();

        public IEnumerable<CategoryRecentBooksModel> GetMostRecentBooks()
            => this.db
            .Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryRecentBooksModel
            {
                Category = c.Name,
                Books = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate.Value.Year)
                    .Take(3)
                    .Select(bc => new TitleReleaseYearBookModel
                    {
                        Title = bc.Book.Title,
                        Year = bc.Book.ReleaseDate.Value.Year
                    })
                .ToList()
            })
            .ToList();
    }
}