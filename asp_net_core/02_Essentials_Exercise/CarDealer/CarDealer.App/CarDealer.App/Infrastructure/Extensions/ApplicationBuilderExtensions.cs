namespace CarDealer.App.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Middleware;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();
    }
}
