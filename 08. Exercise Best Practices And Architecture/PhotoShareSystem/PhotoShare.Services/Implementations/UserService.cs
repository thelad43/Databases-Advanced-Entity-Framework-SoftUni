namespace PhotoShare.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly PhotoShareDbContext db;

        public UserService(PhotoShareDbContext db)
        {
            this.db = db;
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            var user = this.db
                .Users
                .Find(userId);

            var friend = this.db
                .Users
                .Find(friendId);

            var friendship = this.db
                .Friendships
                .FirstOrDefault(f => f.FriendId == userId && f.UserId == friendId);

            user.FriendsAdded.Add(friendship);
            this.db.SaveChanges();

            return friendship;
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var user = this.db
                .Users
                .Find(userId);

            var friend = this.db
                .Users
                .Find(friendId);

            var friendship = new Friendship
            {
                User = user,
                UserId = userId,
                Friend = friend,
                FriendId = friendId
            };

            user.FriendsAdded.Add(friendship);

            this.db.Add(friendship);
            this.db.SaveChanges();

            return friendship;
        }

        public TModel ById<TModel>(int id)
            => this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public TModel ByUsername<TModel>(string username)
            => this.db
                .Users
                .Where(u => u.Username == username)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public void ChangePassword(int userId, string password)
        {
            var user = this.db
                .Users
                .Find(userId);

            user.Password = password;
            this.db.SaveChanges();
        }

        public void Delete(string username)
        {
            var user = this.db
                .Users
                .FirstOrDefault(u => u.Username == username);

            user.IsDeleted = true;

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db
                .Users
                .Any(u => u.Id == id);

        public bool Exists(string username)
            => this.db
                .Users
                .Any(u => u.Username == username);

        public User Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
            };

            this.db.Add(user);
            this.db.SaveChanges();

            return user;
        }

        public Friendship GetFriendship(int userId, int friendId)
            => this.db
                .Friendships
                .FirstOrDefault(f => f.FriendId == userId && f.UserId == friendId);

        public void SetBornTown(int userId, int townId)
        {
            var user = this.db
                .Users
                .Find(userId);

            var town = this.db
                .Towns
                .Find(townId);

            user.BornTown = town;
            this.db.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            var user = this.db
                  .Users
                  .Find(userId);

            var town = this.db
                .Towns
                .Find(townId);

            user.CurrentTown = town;
            this.db.SaveChanges();
        }

        public TModel ByUsernameAndPassword<TModel>(string username, string password)
            => this.db
                .Users
                .Where(u => u.Username == username && u.Password == password)
                .ProjectTo<TModel>()
                .FirstOrDefault();
    }
}