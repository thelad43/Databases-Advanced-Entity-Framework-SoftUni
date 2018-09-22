namespace PhotoShare.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using PhotoShare.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareDbContext db;

        public AlbumService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public TModel ById<TModel>(int id)
            => this.db
                .Albums
                .Where(a => a.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public TModel ByName<TModel>(string name)
            => this.db
                .Albums
                .Where(a => a.Name == name)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public Album Create(int userId, string albumTitle, string bgColor, string[] tags)
        {
            var color = (Color)Enum.Parse(typeof(Color), bgColor);

            var albumTags = new List<AlbumTag>();

            var album = new Album();

            foreach (var tag in tags)
            {
                albumTags.Add(
                    new AlbumTag
                    {
                        Album = album,
                        AlbumId = album.Id,
                        Tag = new Tag { Name = tag }
                    });
            }

            album.Name = albumTitle;
            album.BackgroundColor = color;
            album.AlbumTags = albumTags;

            this.db.Add(album);
            this.db.SaveChanges();

            return album;
        }

        public bool Exists(int id)
            => this.db
                .Albums
                .Any(a => a.Id == id);

        public bool Exists(string name)
            => this.db
                .Albums
                .Any(a => a.Name == name);
    }
}