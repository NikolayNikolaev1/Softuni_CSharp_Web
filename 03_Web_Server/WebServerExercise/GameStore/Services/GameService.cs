namespace GameStore.Services
{
    using Application.ViewModels.Game.Admin;
    using Contracts;
    using Data;
    using Data.Models;
    using GameStore.Application.ViewModels.Game;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameService : IGameService
    {
        public ICollection<GameListingViewModel> All()
        {
            ICollection<GameListingViewModel> gameModels = new List<GameListingViewModel>();

            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                foreach (Game game in dbContext.Games)
                {
                    gameModels.Add(new GameListingViewModel
                    {
                        Id = game.Id,
                        Title = game.Title,
                        Price = game.Price,
                        Size = game.Size
                    });
                }
            }

            return gameModels;
        }

        public void Create(
            string title,
            decimal price,
            double size,
            string trailer,
            string thumbnailUrl,
            string description)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                dbContext
                    .Games
                    .Add(new Game
                    {
                        Title = title,
                        Price = price,
                        Size = size,
                        Trailer = trailer,
                        ImageThumbnail = thumbnailUrl,
                        Description = description,
                        ReleaseDate = DateTime.Now
                    });
                dbContext.SaveChanges();
            }
        }

        public GameDetailsViewModel Find(int id)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                return dbContext
                    .Games
                    .Where(g => g.Id == id)
                    .Select(g => new GameDetailsViewModel
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Trailer = g.Trailer,
                        Description = g.Description,
                        Price = g.Price,
                        Size = g.Size,
                        ReleaseDate = g.ReleaseDate
                    }).FirstOrDefault();
            }
        }
    }
}
