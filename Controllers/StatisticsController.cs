using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Text.Json;


namespace WebApplication1.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly WebApplication1Context _context;

        public StatisticsController(WebApplication1Context context)
        {
            _context = context;
        }
        [HttpGet]
        public JsonResult InnerGet()
        {
            var reservations = _context.Reservations.ToList();
            var result = reservations.GroupBy(u => u.ReservationDate, (Key, Items) => new
            {
                date = Key.ToString(),
                reservationsCount = Items.Count()

            }).ToList();
            return Json(result);
        }
        public IActionResult graph2()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Get()
        {
            var restaurants = _context.Restaurant.ToList();
            var result = restaurants.GroupBy(u => u.City, (key, items) => new RestaurantCityViewModel
            {
                CityName = key,
                NumberOfRestaurants = items.Count()
            }).ToList();
            return Json(result);
        }
        public IActionResult graph1()
        {
            return View();
        }
        public IActionResult Index1()
        {
            var restaurants = _context.Restaurant.ToList();
            var result = restaurants.GroupBy(u => u.City, (key, items) => new RestaurantCityViewModel
            {
                CityName = key,
                NumberOfRestaurants = items.Count()
            }).ToList();

            var model = new GroupRestCityViewModel
            {
                Items = result
            };

            return View(model);
        }
        public IActionResult Index2()
        {
            // test inner
            var InnerTest = (from Restaurant in _context.Restaurant
                             join Reservation in _context.Reservations on Restaurant.RestaurantId equals Reservation.RestaurantID
                             select new ReservationDetailsViewModel
                             {
                                 RestaurantName = Restaurant.Name,
                                 UserName = Reservation.user.LastName,
                                 Date = Reservation.ReservationDate,
                                 NumberOfPeople = Reservation.NumberOfPeople
                             }).ToList();
            var result = InnerTest.GroupBy(u => u.RestaurantName, (key, items) => new InnerViewModel
            {
                RestaurantName = key,
                Items = items.ToList()

            }).ToList();
            var model = new GroupViewModel
            {
                Items = result
            };

            return View(model);
        }
    }
}
