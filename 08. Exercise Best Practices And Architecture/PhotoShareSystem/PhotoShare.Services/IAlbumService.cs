namespace PhotoShare.Services
{
    using Models;

    public interface IAlbumService
    {
        Album Create(int userId, string albumTitle, string bgColor, string[] tags);

        TModel ById<TModel>(int id);

        TModel ByName<TModel>(string name);

        bool Exists(int id);

        bool Exists(string name);
    }
}