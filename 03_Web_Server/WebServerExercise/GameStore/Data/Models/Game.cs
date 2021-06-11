namespace GameStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Trailer { get; set; }

        public string ImageThumbnail { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
