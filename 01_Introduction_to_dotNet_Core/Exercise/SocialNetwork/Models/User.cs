namespace SocialNetwork.Models
{
    using SocialNetwork.Validations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [Password]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(([A-Za-z\d]+)[-_.]{0,1})*([A-Za-z\d]+)@(([A-Za-z\d]+)[.]){1,}([A-Za-z\d]+)$")] // custom regex
        public string Email { get; set; }

        [MaxLength(1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<Friendship> FromFriends { get; set; } = new List<Friendship>();

        public List<Friendship> ToFriends { get; set; } = new List<Friendship>();

    }
}
