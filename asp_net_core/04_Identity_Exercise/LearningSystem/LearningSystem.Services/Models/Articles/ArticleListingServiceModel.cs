namespace LearningSystem.Services.Models.Articles
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System;

    public class ArticleListingServiceModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public User Author { get; set; }
    }
}
