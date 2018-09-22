namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using Services;
    using System;
    using System.Linq;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IUserService users;
        private readonly IAlbumService albums;
        private readonly IAlbumRoleService albumRoles;
        private readonly ISessionService session;

        public ShareAlbumCommand(IUserService users, IAlbumService albums, IAlbumRoleService albumRoles, ISessionService session)
        {
            this.users = users;
            this.albums = albums;
            this.albumRoles = albumRoles;
            this.session = session;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var albumId = int.Parse(data[1]);
            var username = data[2];
            var permissionAsString = data[3];

            var album = this.albums.ById<Album>(albumId);

            if (album == null)
            {
                throw new ArgumentException(string.Format(AlbumDoesNotExist, albumId));
            }

            var user = this.users.ByUsername<User>(username);

            Validator.ThrowExceptionIfUserNotFound(user);

            var parsed = Enum.TryParse(typeof(Role), permissionAsString, out var permission);

            if (!parsed)
            {
                throw new ArgumentException(PermissionNotValidExceptionMessage);
            }

            if (!album.AlbumRoles.Any(r => r.UserId == user.Id && r.Role == Role.Owner))
            {
                throw new InvalidOperationException(InvalidCredentialsExceptionMessage);
            }

            this.albumRoles.PublishAlbumRole(albumId, user.Id, permissionAsString);

            return string.Format(SuccessShareAlbum, username, album.Name, permissionAsString);
        }
    }
}