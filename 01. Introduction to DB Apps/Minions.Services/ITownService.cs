namespace Minions.Services
{
    using Minions.Models;
    using System.Collections.Generic;

    public interface ITownService
    {
        int ChangeTownNamesToUppercase(string countryName);

        IEnumerable<Town> TownsByCountryName(string countryName);
    }
}