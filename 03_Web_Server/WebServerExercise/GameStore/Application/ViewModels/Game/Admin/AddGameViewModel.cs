using System;

namespace GameStore.Application.ViewModels.Game.Admin
{
    public class AddGameViewModel
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string Trailer { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
