namespace BookShop.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<string> GetAuthorNamesEndingIn(string search)
        {
            var names = this.db
                .Authors
                .Where(a => a.FirstName.EndsWith(search))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToList();

            return names;
        }
    }
}