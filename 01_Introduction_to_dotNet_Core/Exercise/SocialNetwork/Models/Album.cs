namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public List<PictureAlbum> Pictures { get; set; } = new List<PictureAlbum>();
    }
}
