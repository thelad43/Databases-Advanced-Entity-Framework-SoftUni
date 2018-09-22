namespace PhotoShare.Common
{
    public static class ExceptionMessages
    {
        public const string PasswordsDoNotMatchExceptionMessage = "Passwords do not match!";
        public const string UsernameIsTakenExceptionMessage = "Username {0} is already taken!";
        public const string TownExistsExceptionMessage = "Town {0} was already added!";
        public const string UserDoesNotExistExceptionMessage = "User {0} not found!";
        public const string PropertyNotFoundExceptionMessage = "Property {0} not supported!";
        public const string InvalidPasswordExceptionMessage = "Invalid Password! Password must contain at least one lowercase letter and digit!";
        public const string ValueNotValidForThatPropertyExceptionMessage = "Value {0} not valid. {1}";
        public const string TownNotFoundExceptionMessage = "Town {0} not found!";
        public const string UserNotFoundExceptionMessage = "User {0} not found!";
        public const string UserIsAlreadyDeletedExceptionMessage = "User {0} is already deleted!";
        public const string TagAlreadyExistsExceptionMessage = "Tag {0} exists!";
        public const string AlbumDoesExistExceptionMessage = "Album {0} exists!";
        public const string BackgroundColorDoesNotExistExceptionMessage = "Color {0} not found!";
        public const string InvalidTagsExceptionMessage = "Invalid tags!";
        public const string AlbumOrTagDoesNotExistExceptionMessage = "Either tag or album do not exist!";
        public const string TheyAreAlreadyFriendsExceptionMessage = "{0} is already a friend to {1}";
        public const string ThereIsNoSuchFriendRequestExceptionMessage = "{0} has not added {1} as a friend";
        public const string PermissionNotValidExceptionMessage = @"Permission must be either ""Owner"" or ""Viewer""!";
        public const string AlbumDoesNotExist = "Album {0} not found!";
        public const string InvalidUsernameOrPasswordExceptionMessage = "Invalid username or password!";
        public const string InvalidCredentialsExceptionMessage = "Invalid credentials!";
    }
}