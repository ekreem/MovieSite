using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetFlix.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MovieActor>()
        //        .HasKey(ma => new { ma.MovieId, ma.ActorId });

        //    modelBuilder.Entity<MovieActor>()
        //        .HasOne(ma => ma.Movie)
        //        .WithMany(m => m.MovieActors)
        //        .HasForeignKey(ma => ma.MovieId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<MovieActor>()
        //        .HasOne(ma => ma.Actor)
        //        .WithMany(a => a.MovieActors)
        //        .HasForeignKey(ma => ma.ActorId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
        public DbSet<Movie> Movies { get; set; }
      
        public DbSet<Actor> Actors { get; set; }
  

     
    }
}
