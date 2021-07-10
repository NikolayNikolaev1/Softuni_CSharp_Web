namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
