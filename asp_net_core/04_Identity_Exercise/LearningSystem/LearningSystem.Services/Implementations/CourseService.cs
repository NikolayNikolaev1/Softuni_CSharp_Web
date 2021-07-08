namespace LearningSystem.Services.Implementations
{
    using Data;
    using System;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(
            string name,
            string description,
            string trainerId,
            DateTime startDate,
            DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
