namespace GameStore.Application.ViewModels.Game.Admin
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants;

    public class EditGameViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        [MinLength(3, ErrorMessage = ErrorMessages.InvalidGameTitle)]
        [MaxLength(100, ErrorMessage = ErrorMessages.InvalidGameTitle)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        public double Size { get; set; }

        [StringLength(11, ErrorMessage = ErrorMessages.InvalidGameTrailer)]
        public string Trailer { get; set; }

        [Url(ErrorMessage = ErrorMessages.InvalidGameThumbnailUrl)]
        public string ThumbnailUrl { get; set; }

        [MinLength(20, ErrorMessage = ErrorMessages.InvalidGameDescription)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
