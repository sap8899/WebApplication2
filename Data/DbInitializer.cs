using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            IdentityUser userResult = userManager.FindByEmailAsync("sapfed@gmail.com").Result;
            IdentityUser user = new IdentityUser
            {
                UserName = "sapir",
                Email = "sapfed@gmail.com",
            };
            if (userResult == null)
            {
                userManager.CreateAsync(user, "ICanSeeU!@12").Wait();
            }
            userManager.AddToRoleAsync(user, "Site Admin").Wait();
            userManager.AddToRoleAsync(user, "Restaurant Owner").Wait();

            IdentityUser tempUser2 = userManager.FindByEmailAsync("sagkap@gmail.com").Result;
            IdentityUser user2 = new IdentityUser
            {
                UserName = "sagi",
                Email = "sagkap@gmail.com",
            };
            if (tempUser2 == null)
            {
                userManager.CreateAsync(user2, "ICanSeeU!@12").Wait();
            }
            userManager.AddToRoleAsync(user2, "Restaurant Owner").Wait();

            IdentityUser tempUser3 = userManager.FindByEmailAsync("benben@gmail.com").Result;
            IdentityUser user3 = new IdentityUser
            {
                UserName = "ben",
                Email = "benben@gmail.com",
            };
            if (tempUser3 == null)
            {
                userManager.CreateAsync(user3, "ICanSeeU!@12").Wait();
            }
            userManager.AddToRoleAsync(user3, "Restaurant Owner").Wait();

            IdentityUser tempUser4 = userManager.FindByEmailAsync("ilaila@gmail.com").Result;
            IdentityUser user4 = new IdentityUser
            {
                UserName = "ilay",
                Email = "ilaila@gmail.com",
            };
            if (tempUser4 == null)
            {
                userManager.CreateAsync(user4, "ICanSeeU!@12").Wait();
            }
            userManager.AddToRoleAsync(user4, "Restaurant Owner").Wait();
        }

        public static void Initialize(WebApplication1Context context)
        {
            context.Database.EnsureCreated();

            if (context.Restaurant.Any())
            {
                return;
            }
            var categories = new Category[]
            {
                new Category{Name="Asian"},
                new Category{Name="Italian"},
                new Category{Name="Mexican"},
                new Category{Name="Cafe"}
            };
            foreach (Category c in categories)
            {
                context.Category.Add(c);
            }
            context.SaveChanges();

            var cities = new City[]
            {
                new City{Name="Tel Aviv"},
                new City{Name="Rishon Lezion"},
                new City{Name="Rehovot"},
                new City{Name="Raanana"}
            };
            foreach (City c in cities)
            {
                context.City.Add(c);
            }
            context.SaveChanges();

            var users = new User[]
            {           
                new User{Email="sapfed@gmail.com", FirstName="sapir", LastName="federovsky"},
                new User{Email="sagkap@gmail.com", FirstName="sagi", LastName="kaplanski"},
                new User{Email="benben@gmail.com", FirstName="ben", LastName="korman"},
                new User{Email="ilaila@gmail.com", FirstName="ilay", LastName="chait"}
            };
            foreach (User u in users)
            {
                context.TestUsers.Add(u);
            }
            context.SaveChanges();

            var restaurants = new Restaurant[]
            {
                new Restaurant{Name="ChopChop", Category="Asian", City="Tel Aviv", Address="Even Gvirol"},
                new Restaurant{Name="Vivino",  Category="Italian",City="Rishon Lezion", Address="Moshe Beker"},
                new Restaurant{Name="Mexicana", Category="Mexican", City="Rehovot", Address="Park Hamada"},
                new Restaurant{Name="Landwer", Category="Cafe", City="Raanana", Address="Hasadna"}
            };
            foreach (Restaurant r in restaurants)
            {
                context.Restaurant.Add(r);
            }
            context.SaveChanges();
            
            var managers = new Manager[]
            {
                new Manager{FirstName="sapir", LastName="federovsky",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "ChopChop").RestaurantId},
                new Manager{FirstName="sagi", LastName="kaplanski",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "Vivino").RestaurantId},
                new Manager{FirstName="ben", LastName="korman",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "Mexicana").RestaurantId},
                new Manager{FirstName="ilay", LastName="hait",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "Landwer").RestaurantId}
            };  
            foreach (Manager m in managers)
            {
                context.Managers.Add(m);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{CityId = cities.FirstOrDefault(r => r.Name == "Tel Aviv").Id,
                    RestaurantId = restaurants.FirstOrDefault(r => r.Name == "ChopChop").RestaurantId,
                    CategoryId = categories.FirstOrDefault(r => r.Name == "Asian").Id},
                new Enrollment{CityId = cities.FirstOrDefault(r => r.Name == "Rishon Lezion").Id,
                    RestaurantId = restaurants.FirstOrDefault(r => r.Name == "Vivino").RestaurantId,
                     CategoryId = categories.FirstOrDefault(r => r.Name == "Italian").Id},
                new Enrollment{CityId = cities.FirstOrDefault(r => r.Name == "Rehovot").Id,
                    RestaurantId = restaurants.FirstOrDefault(r => r.Name == "Mexicana").RestaurantId,
                    CategoryId = categories.FirstOrDefault(r => r.Name == "Mexican").Id},
                new Enrollment{CityId = cities.FirstOrDefault(r => r.Name == "Raanana").Id,
                    RestaurantId = restaurants.FirstOrDefault(r => r.Name == "Landwer").RestaurantId,
                    CategoryId = categories.FirstOrDefault(r => r.Name == "Cafe").Id}
            };
            foreach (Enrollment e in enrollments)
            {

                    context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
