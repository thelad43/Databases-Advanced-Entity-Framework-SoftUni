namespace Minions.Services.Implementations
{
    using Data;
    using Minions.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly MinionsDbContext db;

        public TownService(MinionsDbContext db)
        {
            this.db = db;
        }

        public int ChangeTownNamesToUppercase(string countryName)
        {
            var country = this.db
                .Countries
                .Where(c => c.Name == countryName)
                .Select(c => new CountryModel
                {
                    Name = c.Name,
                    Towns = c.Towns
                })
                .FirstOrDefault();

            if (country == null)
            {
                throw new InvalidOperationException($"Invalid country {countryName}");
            }

            var changedTowns = 0;

            foreach (var town in country.Towns)
            {
                town.Name = town.Name.ToUpper();
                changedTowns++;
            }

            return changedTowns;
        }

        public IEnumerable<Town> TownsByCountryName(string countryName)
        {
            return this.db.Towns.Where(c => c.Country.Name == countryName).ToList();
        }
    }
}