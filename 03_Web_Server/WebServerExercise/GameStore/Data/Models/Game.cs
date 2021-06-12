namespace GameStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [StringLength(11)]
        public string Trailer { get; set; }

        public string ImageThumbnail { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [MinLength(20)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
