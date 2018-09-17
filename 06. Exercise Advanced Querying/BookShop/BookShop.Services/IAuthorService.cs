namespace BookShop.Services
{
    using System.Collections.Generic;

    public interface IAuthorService
    {
        IEnumerable<string> GetAuthorNamesEndingIn(string search);
    }
}