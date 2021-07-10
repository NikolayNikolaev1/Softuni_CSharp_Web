namespace LearningSystem.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<LearningSystemDbContext>()
                    .Database
                    .Migrate();

                UserManager<User> userManager = serviceScope
                    .ServiceProvider
                    .GetService<UserManager<User>>();
                RoleManager<Role> roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<Role>>();

                Task.Run(async () =>
                {
                    var roleNames = typeof(Roles).GetFields();

                    foreach (var roleName in roleNames)
                    {
                        await CreateRoleAsync(userManager, roleManager, roleName.Name);
                    }
                }).Wait();
            }

            return app;
        }

        private static async Task CreateRoleAsync(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            string roleName)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = roleName
                });
            }

            if (roleName.Equals(Roles.Administrator))
            {
                await CreateAdminAsync(userManager);
            }
        }

        private static async Task CreateAdminAsync(UserManager<User> userManager)
        {
            User adminUser = await userManager.FindByEmailAsync(AdminCredentials.Email);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    Email = AdminCredentials.Email,
                    UserName = AdminCredentials.Username,
                    Name = AdminCredentials.Username
                };

                await userManager.CreateAsync(adminUser, AdminCredentials.Password);
                await userManager.AddToRoleAsync(adminUser, Roles.Administrator);
            }
        }
    }
}
