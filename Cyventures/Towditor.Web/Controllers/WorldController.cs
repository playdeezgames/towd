using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Towditor.Web.EFModel;

namespace Towditor.Web.Controllers
{
    [Authorize]
    public class WorldController : Controller
    {
        private readonly TOWDContext _context;

        public WorldController(TOWDContext context)
        {
            _context = context;
        }

        // GET: World
        public async Task<IActionResult> Index()
        {
            return View(await _context.Worlds.ToListAsync());
        }

        // GET: World/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worlds = await _context.Worlds
                .FirstOrDefaultAsync(m => m.WorldId == id);
            if (worlds == null)
            {
                return NotFound();
            }

            return View(worlds);
        }

        // GET: World/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: World/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorldId,WorldName")] Worlds worlds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worlds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worlds);
        }

        // GET: World/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worlds = await _context.Worlds.FindAsync(id);
            if (worlds == null)
            {
                return NotFound();
            }
            return View(worlds);
        }

        // POST: World/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorldId,WorldName")] Worlds worlds)
        {
            if (id != worlds.WorldId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worlds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldsExists(worlds.WorldId))
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
            return View(worlds);
        }

        // GET: World/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worlds = await _context.Worlds
                .FirstOrDefaultAsync(m => m.WorldId == id);
            if (worlds == null)
            {
                return NotFound();
            }

            return View(worlds);
        }

        // POST: World/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worlds = await _context.Worlds.FindAsync(id);
            _context.Worlds.Remove(worlds);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorldsExists(int id)
        {
            return _context.Worlds.Any(e => e.WorldId == id);
        }
    }
}
