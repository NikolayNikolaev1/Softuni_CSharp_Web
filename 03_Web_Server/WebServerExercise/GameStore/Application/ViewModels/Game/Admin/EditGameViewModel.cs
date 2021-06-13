namespace GameStore.Application.ViewModels.Game.Admin
{
    using System;

    public class EditGameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Price { get; set; }

        public string Size { get; set; }

        public string Trailer { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public string ReleaseDate { get; set; }
    }
}
