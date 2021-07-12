namespace LearningSystem.Services.Models.Courses
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using System;

    public class CourseListingServiceModel : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerName { get; set; }


        public void ConfigureMapping(Profile profile)
            => profile.CreateMap<Course, CourseListingServiceModel>()
                .ForMember(c => c.TrainerName, cfg => cfg.MapFrom(c => c.Trainer.Name));
    }
}
