namespace LearningSystem.Services
{
    using System;

    public interface ICourseService
    {
        void Create(
            string name,
            string description,
            string trainerId,
            DateTime startDate,
            DateTime endDate);
    }
}
