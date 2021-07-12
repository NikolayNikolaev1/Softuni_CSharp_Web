namespace LearningSystem.Services.Models.Users
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class UserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<string> RoleNames { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile.CreateMap<User, UserListingServiceModel>()
                .ForMember(u => u.RoleNames, cfg => cfg.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToList()));
    }
}
