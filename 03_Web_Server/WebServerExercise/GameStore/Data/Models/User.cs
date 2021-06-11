namespace GameStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
