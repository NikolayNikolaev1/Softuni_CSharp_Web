namespace GameStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Application.Infrastructure.Constants;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(Validations.Game.TitleMinLength)]
        [MaxLength(Validations.Game.TitleMaxLength)]
        public string Title { get; set; }

        [StringLength(Validations.Game.TrailerIdLegth)]
        public string Trailer { get; set; }

        public string ImageThumbnail { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [MinLength(Validations.Game.DescriptionMinLegth)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
