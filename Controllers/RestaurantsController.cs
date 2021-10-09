using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly WebApplication1Context _context;

        public RestaurantsController(WebApplication1Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Restaurants
        public async Task<IActionResult> Index(string RestaurantCategory,string searchString)
        {
            // Use LINQ to get list of categories.
            IQueryable<string> genreQuery = from r in _context.Restaurant
                                            orderby r.Category
                                            select r.Category;

            var rest = from r in _context.Restaurant
                       select r;
            if (!string.IsNullOrEmpty(searchString))
            {
                rest = rest.Where(s => s.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(RestaurantCategory))
            {
                rest = rest.Where(x => x.Category == RestaurantCategory);
            }

            var RestaurantCatrgoryVM = new CategoryViewModel
            {
                Categories = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Restaurants = await rest.ToListAsync()
            };

            return View(RestaurantCatrgoryVM);
        }
        [Authorize(Roles = "Restaurant Owner")]
        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var managerName = _context.Managers.SingleOrDefault(c => c.RestaurantId == id).FirstName;
            if (currentUserName.ToString() != managerName.ToString())
            {
                return View("AccessDenied", "Shared");
            }
            var results = from r in _context.Restaurant
                          join p in _context.TestReservations on r.RestaurantId equals p.RestaurantID
                          where r.RestaurantId == id
                          orderby p.User
                          select new InnerDetailsViewModel
                          {
                              UserName = p.User,
                              NumberOfPeople = p.NumberOfPeople,
                              Date = p.ReservationDate,
                              RestaurantName = r.Name,
                              Category = r.Category,
                              Address = r.Address,
                              City = r.City
                          };
            var viewModel = new DetailsViewModel { Items = results.ToList() };
            if(!viewModel.Items.Any())
            {
                return View();
            }

            return View(viewModel);
        }
       
        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Category, "Name", "Name");
            ViewData["City"] = new SelectList(_context.City, "Name", "Name");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,Name,Category,City,Address")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
                var first = _context.TestUsers.SingleOrDefault(c => c.FirstName == currentUserName).FirstName;
                var last = _context.TestUsers.SingleOrDefault(c => c.FirstName == currentUserName).LastName;
                var manager = new Manager();
                manager.FirstName = first.ToString();
                manager.LastName = last.ToString();
                manager.RestaurantId = restaurant.RestaurantId;
                manager.Restaurant = restaurant;
                await _context.SaveChangesAsync();
                
                restaurant.Manager = manager;
                _context.Add(restaurant);
                _context.SaveChanges();

                var enrollment = new Enrollment();
                enrollment.CityId = _context.City.SingleOrDefault(c => c.Name == restaurant.City).Id;
                enrollment.CategoryId = _context.Category.SingleOrDefault(c => c.Name == restaurant.Category).Id;
                enrollment.RestaurantId = _context.Restaurant.SingleOrDefault(c => c.RestaurantId == restaurant.RestaurantId).RestaurantId;
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Category, "Name", "Name", restaurant.Category);
            ViewData["City"] = new SelectList(_context.City, "Name", "Name", restaurant.City);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantId,Name,Category,City,Address")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var managerName = _context.Managers.SingleOrDefault(c => c.RestaurantId == id).FirstName;
            if (currentUserName.ToString() != managerName.ToString())
            {
                return View("AccessDenied", "Shared");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var managerName = _context.Managers.SingleOrDefault(c => c.RestaurantId == id).FirstName;
            if (currentUserName.ToString() != managerName.ToString())
            {
                return View("AccessDenied", "Shared");
            }
            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var managerName = _context.Managers.SingleOrDefault(c => c.RestaurantId == id).FirstName;
            if (currentUserName.ToString() != managerName.ToString())
            {
                return View("AccessDenied", "Shared");
            }
            var enrollment = _context.Enrollments.SingleOrDefault(e => e.RestaurantId == id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            var manager = _context.Managers.SingleOrDefault(m => m.RestaurantId == id);
            if (manager != null)
            {
                _context.Managers.Remove(manager);
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.RestaurantId == id);
        }
    }
}
