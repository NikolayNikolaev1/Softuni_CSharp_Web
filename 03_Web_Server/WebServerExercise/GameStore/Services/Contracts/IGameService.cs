namespace GameStore.Services.Contracts
{
    using Application.ViewModels.Game;
    using Application.ViewModels.Game.Admin;
    using System.Collections.Generic;

    public interface IGameService
    {
        ICollection<GameListingViewModel> All();

        void Create(string title, decimal price, double size, string trailer, string thumbnailUrl, string description);

        GameDetailsViewModel Find(int id);
    }
}
