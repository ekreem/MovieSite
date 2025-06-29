using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetFlix.BLL.Services.Abstracts;
using NetFlix.BLL.Services.Concretes;
using NetFlix.CORE.Entities;
using NetFlix.DAL.Contexts;
using NetFlix.DAL.Repositories.Abstracts;
using NetFlix.DAL.Repositories.Concretes;
using System;

namespace NetFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<AppUser,IdentityRole>(opt=> 
                opt.User.RequireUniqueEmail = true
           ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IAccountService, AccountService>();  
            

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

            

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
