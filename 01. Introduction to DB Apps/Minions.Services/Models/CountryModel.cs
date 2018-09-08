namespace Minions.Services.Models
{
    using Minions.Models;
    using System.Collections.Generic;

    public class CountryModel
    {
        public string Name { get; set; }

        public IEnumerable<Town> Towns { get; set; }
    }
}