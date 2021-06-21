namespace GameStore.Services.Contracts
{
    using Application.ViewModels.Game;
    using Application.ViewModels.Game.Admin;
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public interface IGameService
    {
        ICollection<GameListingViewModel> All();

        bool Contains(int id);

        void Create(
            string title,
            decimal price,
            double size, 
            string trailer, 
            string thumbnailUrl, 
            string description, 
            DateTime releaseDate);

        void Delete(int id);

        GameDetailsViewModel Details(int id);

        void Edit(
            int id,
            string title,
            decimal price,
            double size,
            string trailer,
            string thumbnailUrl,
            string description,
            DateTime releaseDate);
            
        Game Find(int id);

    }
}
