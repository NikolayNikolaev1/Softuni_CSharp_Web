namespace GameStore.Application.ViewModels.Game.Admin
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Infrastructure.Constants;

    public class AddGameViewModel
    {
        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        [MinLength(Validations.Game.TitleMinLength, ErrorMessage = ErrorMessages.InvalidGameTitle)]
        [MaxLength(Validations.Game.TitleMaxLength, ErrorMessage = ErrorMessages.InvalidGameTitle)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmptyFields)]
        [MinLength(0, ErrorMessage = ErrorMessages.InvalidGameSize)]
        public double Size { get; set; }

        [StringLength(Validations.Game.TrailerIdLegth, ErrorMessage = ErrorMessages.InvalidGameTrailer)]
        public string Trailer { get; set; }

        [Url(ErrorMessage = ErrorMessages.InvalidGameThumbnailUrl)]
        public string ThumbnailUrl { get; set; }

        [MinLength(Validations.Game.DescriptionMinLegth, ErrorMessage = ErrorMessages.InvalidGameDescription)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
