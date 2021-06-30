namespace CameraBazaar.Data.Models
{
    using Validations.User;

    public class User
    {
        public int Id { get; set; }

        [Username(nameof(Username))]
        public string Username { get; set; }

        [Email]
        public string Email { get; set; }

        [Password(nameof(Password))]
        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
