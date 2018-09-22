namespace PhotoShare.Services.Implementations
{
    using Data;
    using Models;
    using Models.Enums;
    using System;

    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareDbContext db;

        public AlbumRoleService(PhotoShareDbContext context)
        {
            this.db = context;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
        {
            var roleAsEnum = (Role)Enum.Parse(typeof(Role), role);

            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = roleAsEnum
            };

            this.db.AlbumRoles.Add(albumRole);

            this.db.SaveChanges();

            return albumRole;
        }
    }
}