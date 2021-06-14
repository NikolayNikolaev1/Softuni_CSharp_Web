namespace GameStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Application.Infrastructure.Constants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Validations.User.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(Validations.User.PasswordMaxLength)]
        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
