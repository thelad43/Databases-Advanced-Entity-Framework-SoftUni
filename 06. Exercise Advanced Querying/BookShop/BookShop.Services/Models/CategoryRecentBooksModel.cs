namespace BookShop.Services.Models
{
    using System.Collections.Generic;

    public class CategoryRecentBooksModel
    {
        public string Category { get; set; }

        public IEnumerable<TitleReleaseYearBookModel> Books { get; set; }
    }
}