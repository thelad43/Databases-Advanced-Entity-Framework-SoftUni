namespace Minions.Services.Implementations
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VillainService : IVillainService
    {
        private readonly MinionsDbContext db;

        public VillainService(MinionsDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<VillainModel> VillainsAndNumberOfMinions()
        {
            var minions = this.db
                .Villains
                .Select(v => new VillainModel
                {
                    Name = v.Name,
                    MinionsCount = v.Minions.Count
                })
                .Where(v => v.MinionsCount > 3)
                .OrderByDescending(v => v.MinionsCount)
                .ToList();

            return minions;
        }

        public VillainWithMinionsModel VillainWithMinionsById(int id)
        {
            var villainWithMinions = this.db
                .Villains
                .Where(v => v.Id == id)
                .Select(v => new VillainWithMinionsModel
                {
                    VillainName = v.Name,
                    Minions = v.Minions.Select(m => new MinionModel
                    {
                        Name = m.Minion.Name,
                        Age = m.Minion.Age
                    })
                    .ToList()
                })
                .FirstOrDefault();

            if (villainWithMinions == null)
            {
                throw new NullReferenceException($"No villain with ID {id} exists in the database.");
            }

            return villainWithMinions;
        }
    }
}