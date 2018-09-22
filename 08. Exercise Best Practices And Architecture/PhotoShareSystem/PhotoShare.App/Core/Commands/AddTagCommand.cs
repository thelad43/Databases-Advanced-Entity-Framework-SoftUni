namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using Services;
    using System;
    using Utilities;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class AddTagCommand : ICommand
    {
        private readonly ITagService tags;
        private readonly ISessionService session;

        public AddTagCommand(ITagService tags, ISessionService session)
        {
            this.tags = tags;
            this.session = session;
        }

        // AddTag <tag>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var tagName = TagUtilities.ValidateOrTransform(data[1]);

            if (this.tags.Exists(tagName))
            {
                throw new ArgumentException(string.Format(TagAlreadyExistsExceptionMessage, tagName));
            }

            this.tags.AddTag(tagName);

            return string.Format(SuccessAddTagMessage, tagName);
        }
    }
}