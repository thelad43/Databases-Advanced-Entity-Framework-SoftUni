namespace PhotoShare.Services
{
    using Models;

    public interface IAlbumRoleService
    {
        AlbumRole PublishAlbumRole(int albumId, int userId, string role);
    }
}