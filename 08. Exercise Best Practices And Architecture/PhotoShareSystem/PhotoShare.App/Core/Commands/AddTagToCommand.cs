namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using Services;
    using System;
    using System.Linq;
    using Utilities;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albums;
        private readonly ITagService tags;
        private readonly IAlbumTagService albumsTags;
        private readonly ISessionService session;

        public AddTagToCommand(IAlbumService albums, ITagService tags, IAlbumTagService albumsTags, ISessionService session)
        {
            this.albums = albums;
            this.tags = tags;
            this.albumsTags = albumsTags;
            this.session = session;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var albumName = data[1];
            var tagName = TagUtilities.ValidateOrTransform(data[2]);

            var album = this.albums.ByName<Album>(albumName);

            if (!album.AlbumRoles.Any(r => r.UserId == this.session.User.Id && r.Role == Role.Owner))
            {
                throw new InvalidOperationException(InvalidCredentialsExceptionMessage);
            }

            var tag = this.tags.ByName<Tag>(tagName);

            if (album == null || tag == null)
            {
                throw new ArgumentException(AlbumOrTagDoesNotExistExceptionMessage);
            }

            this.albumsTags.AddTagTo(album.Id, tag.Id);
            return string.Format(SuccessAddTagToMessage, tag.Name, album.Name);
        }
    }
}