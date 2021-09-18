﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.Reservations.Include(r => r.Restaurant).Include(r => r.user);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.user)
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,UserID,RestaurantID,NumberOfPeople,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name", reservation.RestaurantID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserId", "FirstName", reservation.UserID);
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

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name", reservation.RestaurantID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserId", "FirstName", reservation.UserID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,UserID,RestaurantID,NumberOfPeople,ReservationDate")] Reservation reservation)
        {
            if (id != reservation.RestaurantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.RestaurantID))
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
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantId", "Name", reservation.RestaurantID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserId", "FirstName", reservation.UserID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Restaurant Owner")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
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
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.RestaurantID == id);
        }
    }
}
