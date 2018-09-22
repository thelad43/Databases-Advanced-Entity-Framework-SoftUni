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

    public class UploadPictureCommand : ICommand
    {
        private readonly IPictureService pictures;
        private readonly IAlbumService albums;
        private readonly ISessionService session;

        public UploadPictureCommand(IPictureService pictures, IAlbumService albums, ISessionService session)
        {
            this.pictures = pictures;
            this.albums = albums;
            this.session = session;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var albumName = data[1];
            var pictureTitle = data[2];
            var path = data[3];

            var albumExists = this.albums.Exists(albumName);

            if (!albumExists)
            {
                throw new ArgumentException(string.Format(AlbumDoesNotExist, albumName));
            }

            var album = this.albums.ByName<Album>(albumName);

            if (!album.AlbumRoles.Any(r => r.UserId == this.session.User.Id && r.Role == Role.Owner))
            {
                throw new InvalidOperationException(InvalidCredentialsExceptionMessage);
            }

            var picture = this.pictures.Create(album.Id, pictureTitle, path);

            return string.Format(SuccessUploadPicture, pictureTitle, albumName);
        }
    }
}