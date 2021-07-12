namespace LearningSystem.Services
{
    using Models.Articles;
    using System;
    using System.Collections.Generic;

    public interface IArticleService
    {
        ICollection<ArticleListingServiceModel> All();

        void Create(string title, string content, DateTime PublishDate, string authorId);
    }
}
