using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : IdentityDbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Restaurant> Restaurant { get; set; }

        public DbSet<WebApplication1.Models.Reservation> TestReservations { get; set; }

        public DbSet<WebApplication1.Models.City> City { get; set; }
        public DbSet<WebApplication1.Models.Category> Category { get; set; }
        public DbSet<WebApplication1.Models.User> TestUsers { get; set; }
        public DbSet<WebApplication1.Models.Manager> Managers { get; set; }
        public DbSet<WebApplication1.Models.Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Manager>().ToTable("Manager");

            modelBuilder.Entity<Reservation>()
                .HasKey(c => new { c.RestaurantID, c.UserToken });
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Site Admin", NormalizedName = "Site Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Restaurant Owner", NormalizedName = "Restaurant Owner".ToUpper() });
        }
    }
}
