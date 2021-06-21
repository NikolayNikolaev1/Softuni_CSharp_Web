namespace ByTheCake.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
