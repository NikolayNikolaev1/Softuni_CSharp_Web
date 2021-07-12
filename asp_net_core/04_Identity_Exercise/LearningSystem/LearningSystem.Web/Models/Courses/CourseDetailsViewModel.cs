namespace LearningSystem.Web.Models.Courses
{
    using Services.Models.Courses;

    public class CourseDetailsViewModel
    {
        public CourseDetailsServiceModel CourseDetails { get; set; }

        public bool IsUserInCourse { get; set; }
    }
}
