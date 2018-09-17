namespace BookShop.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<CategoryProfitModel> GetTotalProfitByCategory();

        IEnumerable<CategoryRecentBooksModel> GetMostRecentBooks();
    }
}