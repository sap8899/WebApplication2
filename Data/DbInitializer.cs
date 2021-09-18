using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbInitializer
    {
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
                new User{Email="Sagkap@gmail.com", FirstName="sagi", LastName="kaplanski"},
                new User{Email="Benben@gmail.com", FirstName="ben", LastName="ben"},
                new User{Email="Ilaila@gmail.com", FirstName="ilay", LastName="ilay"}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
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
                new Manager{FirstName="a", LastName="a",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "ChopChop").RestaurantId},
                new Manager{FirstName="b", LastName="b",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "Vivino").RestaurantId},
                new Manager{FirstName="c", LastName="c",
                    RestaurantId=restaurants.FirstOrDefault(r => r.Name == "Mexicana").RestaurantId},
                new Manager{FirstName="d", LastName="d",
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
