namespace Minions.Services.Models
{
    using System.Collections.Generic;

    public class VillainWithMinionsModel
    {
        public string VillainName { get; set; }

        public IEnumerable<MinionModel> Minions { get; set; }
    }
}