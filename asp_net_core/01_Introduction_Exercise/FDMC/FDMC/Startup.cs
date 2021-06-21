namespace FDMC
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.Run(async (context) =>
            {
                string requestPath = context.Request.Path.ToString();
                string requestMethod = context.Request.Method.ToUpper();

                if (requestPath.Equals("/"))
                {
                    await context.Response.WriteAsync(@"<h1>FDMC</h1>");

                    using (AppDbContext dbContext = new AppDbContext())
                    {
                        IList<Cat> cats = dbContext.Cats.ToList();

                        await context.Response.WriteAsync(@"<ul>");
                        foreach (Cat cat in cats)
                        {
                            await context.Response.WriteAsync(@$"<li><a href=""/cat/{cat.Id}"">{cat.Name}</a></li>");
                        }

                        await context.Response.WriteAsync(@"</ul>");
                    }

                    await context.Response.WriteAsync(@"
<form method=""get"" action=""/cat/add"">
    <input type=""submit"" value=""Add Cat"">
</form>");
                }
                else if (requestPath.Equals("/cat/add") &&
                    requestMethod.Equals("GET"))
                {
                    await context.Response.WriteAsync(@"<h1>Add Cat</h1>");
                    await context.Response.WriteAsync(@"
<form method=""post"" action=""/cat/add"">
    <div >
        <label>Name</label>
        <input type=""text"" name=""name"">
    </div>
    <div >
        <label>Age</label>
        <input type=""number"" name=""age"">
    </div>
    <div >
        <label>Breed</label>
        <input type=""text"" name=""breed"">
    </div>
    <div >
        <label>Image Url</label>
        <input type=""text"" name=""image-url"">
    </div>
    <div>
        <input type=""submit"" value=""Add Cat"">
    </div>
</form>");
                }
                else if (requestPath.Equals("/cat/add") &&
                    requestMethod.Equals("POST"))
                {
                    string catName = context.Request.Form["name"];
                    int catAge = int.Parse(context.Request.Form["age"]);
                    string catBreed = context.Request.Form["breed"];
                    string catImageUrl = context.Request.Form["image-url"];

                    using (AppDbContext dbContext = new AppDbContext())
                    {
                        dbContext.Cats.Add(new Cat
                        {
                            Name = catName,
                            Age = catAge,
                            Breed = catBreed,
                            ImageUrl = catImageUrl
                        });

                        dbContext.SaveChanges();
                    }

                    context.Response.Redirect("/");
                }
                else if (requestPath.StartsWith("/cat") &&
                    char.IsDigit(requestPath.Split("/", System.StringSplitOptions.RemoveEmptyEntries)[1][0]))
                {
                    int catId = int.Parse(requestPath.Split("/", System.StringSplitOptions.RemoveEmptyEntries)[1]);

                    using (AppDbContext dbContext = new AppDbContext())
                    {
                        Cat cat = dbContext.Cats.FirstOrDefault(c => c.Id == catId);

                        await context.Response.WriteAsync(@$"<h1>{cat.Name}</h1>");

                        await context.Response.WriteAsync(@$"<img src=""{cat.ImageUrl}"" style=""max-width: 300px;"" />");

                        await context.Response.WriteAsync(@$"<p>Age: {cat.Age}</p>");
                        await context.Response.WriteAsync(@$"<p>Breed: {cat.Breed}</p>");
                    }
                }

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
