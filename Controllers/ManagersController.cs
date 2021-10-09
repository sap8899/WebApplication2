using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ManagersController : Controller
    {
        private readonly WebApplication1Context _context;

        public ManagersController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var webApplication1Context = _context.Managers.Include(m => m.Restaurant);
            return View(await webApplication1Context.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "RestaurantId", "RestaurantId");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,LastName,FirstName")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "RestaurantId", "RestaurantId", manager.RestaurantId);
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "RestaurantId", "RestaurantId", manager.RestaurantId);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantId,LastName,FirstName")] Manager manager)
        {
            if (id != manager.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.RestaurantId))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "RestaurantId", "RestaurantId", manager.RestaurantId);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.RestaurantId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.RestaurantId == id);
        }
    }
}
