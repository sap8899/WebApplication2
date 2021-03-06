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
    public class ReservationsController : Controller
    {
        private readonly WebApplication1Context _context;

        public ReservationsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Reservations
        [Authorize(Roles = "Site Admin")]
        public async Task<IActionResult> Index(string option, string search)
        {
            //if a user choose the radio button option as Subject  
            if (option == "Restaurant")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(_context.TestReservations.Include(r => r.Restaurant)
                    .Where(x => x.Restaurant.Name.Contains(search) || search == null).ToList());
            }
            else if (option == "City")
            {
                return View(_context.TestReservations.Include(r => r.Restaurant)
                    .Where(x => x.Restaurant.City.Contains(search) || search == null).ToList());
            }
            else if (option == "User")
            {
                return View(_context.TestReservations
                    .Where(x => x.User==(search) || search == null).ToList());
            }
            else
            {
                return View(_context.TestReservations.Include(r => r.Restaurant)
                    .Where(x => x.Restaurant.Category.Contains(search) || search == null).ToList());
            }
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Restaurant Owner,Site Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = await _context.TestReservations
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,User,RestaurantID,NumberOfPeople,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
                var res = new Reservation();
                res.Restaurant = reservation.Restaurant;
                res.NumberOfPeople = reservation.NumberOfPeople;
                res.RestaurantID = reservation.RestaurantID;
                res.User = currentUserName.ToString();
                res.ReservationDate = reservation.ReservationDate;
                res.UserToken = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(res);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "Home");
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name", reservation.RestaurantID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.TestReservations.FirstOrDefaultAsync(r => r.ReservationID==id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name", reservation.RestaurantID);
            ViewData["User"] = new SelectList(_context.Users, "UserName", "UserName", reservation.User);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,User,RestaurantID,NumberOfPeople,ReservationDate")] Reservation reservation)
        {
            
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }
            if (!ReservationExists(reservation.ReservationID))
            {
                return NotFound();
            }
            var new_res = new Reservation();
            new_res.NumberOfPeople = reservation.NumberOfPeople;
            new_res.RestaurantID = _context.TestReservations.AsNoTracking().SingleOrDefault(r => r.ReservationID == id).RestaurantID;
            new_res.User = _context.TestReservations.AsNoTracking().SingleOrDefault(r => r.ReservationID == id).User;
            new_res.ReservationDate = reservation.ReservationDate;
            new_res.Restaurant = _context.TestReservations.AsNoTracking().SingleOrDefault(r => r.ReservationID == id).Restaurant;
            new_res.UserToken = _context.TestReservations.AsNoTracking().SingleOrDefault(r => r.ReservationID == id).UserToken;
            var res = _context.TestReservations.SingleOrDefault(r => r.ReservationID == id);
            _context.TestReservations.Remove(res);
            _context.SaveChanges();
            _context.Add(new_res);
            _context.SaveChanges();
            //return RedirectToAction(nameof(Index));
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant.AsNoTracking(), "RestaurantId", "Name", reservation.RestaurantID);
            ViewData["User"] = new SelectList(_context.Users.AsNoTracking(), "UserName", "UserName", reservation.User);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.TestReservations
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.TestReservations.FirstOrDefaultAsync(m => m.ReservationID == id);
            _context.TestReservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        private bool ReservationExists(int id)
        {
            return _context.TestReservations.Any(e => e.ReservationID == id);
        }
    }
}
