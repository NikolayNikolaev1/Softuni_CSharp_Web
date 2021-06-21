namespace FDMC.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(2048)]
        public string ImageUrl { get; set; }
    }
}
