namespace LearningSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.Properties;

    public class Article
    {
        public int Id { get; set; }

        [MaxLength(ArticleTitleMaxLength)]
        [Required]
        public string Title { get; set; }

        [MaxLength(ArticleContentMaxLength)]
        [Required]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
