namespace PhotoShare.Services
{
    using Models;

    public interface IAlbumTagService
    {
        AlbumTag AddTagTo(int albumId, int tagId);
    }
}