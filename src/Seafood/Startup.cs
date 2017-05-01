using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Seafood.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Seafood
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            AddSeedData(context);
            app.Run(async (context1) =>
            {
                await context1.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddSeedData(ApplicationDbContext context)
        {
            var dummyPost = new Models.Post
            {
                Description = "Alaskan Cod on Sale",
                ImagePath = "https://fthmb.tqn.com/zL2MRlV_In8suQ4N1A26GBTxXFY=/768x0/filters:no_upscale()/about/Black-cod-sablefish-58b8ea473df78c353c297e37.jpg"

            };

            context.Posts.Add(dummyPost);

            var dummyPost2 = new Models.Post
            {
                Description = "Lemon Baked Cod Recipe",
                ImagePath = "http://img.sndimg.com/food/image/upload/h_420,w_560,c_fit/v1/img/recipes/13/52/72/aML6ha46QVqfQuw5Fgu5_THE%20FOOD%20GAYS%20-%20BAKED%20LEMON%20COD.jpg"
            };

            context.Posts.Add(dummyPost2);

            context.SaveChanges();
        }
    }
}
