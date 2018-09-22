namespace PhotoShare.App.Core.Models
{
    using Common;

    public class RegisterUserModel
    {
        public string Username { get; set; }

        [Password(4, 20)]
        public string Password { get; set; }

        [Password(4, 20)]
        public string RepeatPassword { get; set; }

        [Email]
        public string Email { get; set; }
    }
}