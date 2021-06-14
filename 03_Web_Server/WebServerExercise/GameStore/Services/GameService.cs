namespace GameStore.Services
{
    using Application.ViewModels.Game;
    using Application.ViewModels.Game.Admin;
    using Contracts;
    using Data;
    using Data.Models;
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

        public bool Contains(int id)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                if (dbContext.Games.Any(g => g.Id == id))
                {
                    return true;
                }

                return false;
            }
        }

        public void Create(
            string title,
            decimal price,
            double size,
            string trailer,
            string thumbnailUrl,
            string description,
            DateTime releaseDate)
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
                        ReleaseDate = releaseDate
                    });
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                Game game = dbContext
                    .Games
                    .FirstOrDefault(g => g.Id == id);
                dbContext
                    .Games
                    .Remove(game);
            }
        }

        public GameDetailsViewModel Details(int id)
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

        public void Edit(
            int id,
            string title,
            decimal price, 
            double size,
            string trailer, 
            string thumbnailUrl, 
            string description, 
            DateTime releaseDate)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                Game game = dbContext
                    .Games
                    .FirstOrDefault(g => g.Id == id);

                game.Title = title;
                game.Price = price;
                game.Size = size;
                game.Trailer = trailer;
                game.ImageThumbnail = thumbnailUrl;
                game.Description = description;
                game.ReleaseDate = releaseDate;
                dbContext.SaveChanges();
            }
        }

        public Game Find(int id)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                return dbContext
                    .Games
                    .Where(g => g.Id == id)
                    .FirstOrDefault();
            }
        }
    }
}
