namespace PhotoShare.Services.Implementations
{
    using Data;
    using Models;
    using System.Linq;

    public class AlbumTagService : IAlbumTagService
    {
        private readonly PhotoShareDbContext db;

        public AlbumTagService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public AlbumTag AddTagTo(int albumId, int tagId)
        {
            var album = this.db
                .Albums
                .FirstOrDefault(a => a.Id == albumId);

            var tag = this.db
                .Tags
                .FirstOrDefault(t => t.Id == tagId);

            var albumTag = new AlbumTag
            {
                Album = album,
                AlbumId = album.Id,
                Tag = tag,
                TagId = tag.Id
            };

            album.AlbumTags.Add(albumTag);
            this.db.SaveChanges();

            return albumTag;
        }
    }
}