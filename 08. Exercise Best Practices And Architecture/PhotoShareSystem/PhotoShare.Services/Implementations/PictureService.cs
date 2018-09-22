namespace PhotoShare.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PictureService : IPictureService
    {
        private readonly PhotoShareDbContext db;

        public PictureService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByTitle<TModel>(string name)
            => By<TModel>(a => a.Title == name).SingleOrDefault();

        public bool Exists(int id)
            => ById<Picture>(id) != null;

        public bool Exists(string name)
           => ByTitle<Picture>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Picture, bool> predicate)
            => this.db.Pictures
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>();

        public Picture Create(int albumId, string pictureTitle, string pictureFilePath)
        {
            var picture = new Picture()
            {
                Title = pictureTitle,
                Path = pictureFilePath,
                AlbumId = albumId
            };

            this.db.Add(picture);
            this.db.SaveChanges();

            return picture;
        }
    }
}