namespace PhotoShare.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.Linq;

    public class TagService : ITagService
    {
        private readonly PhotoShareDbContext db;

        public TagService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public Tag AddTag(string name)
        {
            var tag = new Tag
            {
                Name = name
            };

            this.db.Add(tag);
            this.db.SaveChanges();

            return tag;
        }

        public TModel ById<TModel>(int id)
            => this.db
                .Tags
                .Where(t => t.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public TModel ByName<TModel>(string name)
            => this.db
                .Tags
                .Where(t => t.Name == name)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public bool Exists(int id)
            => this.db
                .Tags
                .Any(t => t.Id == id);

        public bool Exists(string name)
            => this.db
                .Tags
                .Any(t => t.Name == name);
    }
}