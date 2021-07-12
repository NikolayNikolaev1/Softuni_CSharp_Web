namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Articles;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ArticleService : IArticleService
    {
        private readonly LearningSystemDbContext dbContext;

        public ArticleService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<ArticleListingServiceModel> All()
            => this.dbContext
            .Articles
            .ProjectTo<ArticleListingServiceModel>()
            .OrderByDescending(a => a.PublishDate)
            .ToList();

        public void Create(string title, string content, DateTime publishDate, string authorId)
        {
            this.dbContext
                .Articles
                .Add(new Article
                {
                    Title = title,
                    Content = content,
                    PublishDate = publishDate,
                    AuthorId = authorId
                });
            this.dbContext.SaveChanges();
        }
    }
}
