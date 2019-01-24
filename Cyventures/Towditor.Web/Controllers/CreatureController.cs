using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Towditor.Web.EFModel;

namespace Towditor.Web.Controllers
{
    public class CreatureController : Controller
    {
        private readonly TOWDContext _context;

        public CreatureController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Creature
        public async Task<IActionResult> Index()
        {
            var tOWDContext = _context.Creatures.Include(c => c.Bitmap);
            return View(await tOWDContext.ToListAsync());
        }

        // GET: Creature/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatures = await _context.Creatures
                .Include(c => c.Bitmap)
                .FirstOrDefaultAsync(m => m.CreatureId == id);
            if (creatures == null)
            {
                return NotFound();
            }

            return View(creatures);
        }

        // GET: Creature/Create
        public IActionResult Create()
        {
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View();
        }

        // POST: Creature/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatureId,BitmapId,CreatureName")] Creatures creatures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View(creatures);
        }

        // GET: Creature/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatures = await _context.Creatures.FindAsync(id);
            if (creatures == null)
            {
                return NotFound();
            }
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View(creatures);
        }

        // POST: Creature/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatureId,BitmapId,CreatureName")] Creatures creatures)
        {
            if (id != creatures.CreatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creatures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreaturesExists(creatures.CreatureId))
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
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View(creatures);
        }

        // GET: Creature/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creatures = await _context.Creatures
                .Include(c => c.Bitmap)
                .FirstOrDefaultAsync(m => m.CreatureId == id);
            if (creatures == null)
            {
                return NotFound();
            }

            return View(creatures);
        }

        // POST: Creature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creatures = await _context.Creatures.FindAsync(id);
            _context.Creatures.Remove(creatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreaturesExists(int id)
        {
            return _context.Creatures.Any(e => e.CreatureId == id);
        }
    }
}
