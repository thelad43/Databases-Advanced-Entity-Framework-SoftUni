namespace PhotoShare.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly PhotoShareDbContext db;

        public TownService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public Town Add(string townName, string countryName)
        {
            var town = new Town
            {
                Name = townName,
                Country = countryName
            };

            this.db.Add(town);
            this.db.SaveChanges();

            return town;
        }

        public TModel ById<TModel>(int id)
            => this.db
                .Towns
                .Where(t => t.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public TModel ByName<TModel>(string name)
            => this.db
                .Towns
                .Where(t => t.Name == name)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public bool Exists(int id)
            => this.db
                .Towns
                .Any(t => t.Id == id);

        public bool Exists(string name)
            => this.db
                .Towns
                .Any(t => t.Name == name);
    }
}