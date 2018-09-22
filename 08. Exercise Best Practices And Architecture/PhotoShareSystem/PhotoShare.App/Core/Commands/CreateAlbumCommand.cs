namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using Services;
    using System;
    using System.Linq;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albums;
        private readonly IUserService users;
        private readonly ITagService tags;
        private readonly ISessionService session;

        public CreateAlbumCommand(IAlbumService albums, IUserService users, ITagService tags, ISessionService session)
        {
            this.albums = albums;
            this.users = users;
            this.tags = tags;
            this.session = session;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var username = data[1];
            var albumTitle = data[2];
            var backgroundColor = data[3];

            var tags = data.Skip(4).ToArray();

            var user = this.users.ByUsername<User>(username);

            Validator.ThrowExceptionIfUserIsDeleted(user, username);
            Validator.ThrowExceptionIfUserNotFound(user);

            if (this.albums.Exists(albumTitle))
            {
                throw new ArgumentException(string.Format(AlbumDoesExistExceptionMessage, albumTitle));
            }

            var colorIsValid = Enum.TryParse(typeof(Color), backgroundColor, out var color);

            if (!colorIsValid)
            {
                throw new ArgumentException(string.Format(BackgroundColorDoesNotExistExceptionMessage, backgroundColor));
            }

            var allTagsExist = true;

            foreach (var tag in tags)
            {
                if (!this.tags.Exists(tag))
                {
                    allTagsExist = false;
                }
            }

            if (!allTagsExist)
            {
                throw new ArgumentException(InvalidTagsExceptionMessage);
            }

            this.albums.Create(user.Id, albumTitle, backgroundColor, tags);

            return string.Format(SuccessCreateAlbumMessage, albumTitle);
        }
    }
}