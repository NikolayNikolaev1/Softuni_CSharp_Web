namespace CameraBazaar.App.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        private const string AdministratorRole = "Administrator";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<CameraBazaarDbContext>()
                    .Database
                    .Migrate();

                UserManager<User> userManager = serviceScope
                    .ServiceProvider
                    .GetService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    bool roleExists = await roleManager.RoleExistsAsync(AdministratorRole);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = AdministratorRole
                        });
                    }

                    string adminEmail = "admin@camerabazaar.test";
                    var adminUser = await userManager.FindByEmailAsync(adminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = "admin"
                        };

                        await userManager.CreateAsync(adminUser, "admin123");
                        await userManager.AddToRoleAsync(adminUser, AdministratorRole);
                    }
                }).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
