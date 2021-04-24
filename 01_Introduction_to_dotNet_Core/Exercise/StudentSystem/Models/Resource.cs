namespace StudentSystem.Models
{
    using StudentSystem.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ResourceType TypeOfResource { get; set; }

        [Required]
        public string URL { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<License> Licenses { get; set; } = new List<License>();
    }
}
