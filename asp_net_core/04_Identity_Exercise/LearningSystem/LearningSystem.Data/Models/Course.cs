namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants.Properties;

    public class Course
    {
        public int Id { get; set; }

        [MaxLength(CourseNameMaxLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(CourseDescriptionMaxLength)]
        [Required]
        public string Description { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
