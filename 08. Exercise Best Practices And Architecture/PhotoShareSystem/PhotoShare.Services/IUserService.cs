namespace PhotoShare.Services
{
    using Models;

    public interface IUserService
    {
        TModel ById<TModel>(int id);

        TModel ByUsername<TModel>(string username);

        bool Exists(int id);

        bool Exists(string username);

        User Register(string username, string password, string email);

        void Delete(string username);

        Friendship AddFriend(int userId, int friendId);

        Friendship AcceptFriend(int userId, int friendId);

        TModel ByUsernameAndPassword<TModel>(string username, string password);

        Friendship GetFriendship(int userId, int friendId);

        void ChangePassword(int userId, string password);

        void SetBornTown(int userId, int townId);

        void SetCurrentTown(int userId, int townId);
    }
}