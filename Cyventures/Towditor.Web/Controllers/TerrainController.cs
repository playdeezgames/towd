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
    public class TerrainController : Controller
    {
        private readonly TOWDContext _context;

        public TerrainController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Terrain
        public async Task<IActionResult> Index()
        {
            var tOWDContext = _context.Terrains.Include("Bitmap.BitmapSequence");
            return View(await tOWDContext.ToListAsync());
        }

        // GET: Terrain/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terrains = await _context.Terrains
                .Include("Bitmap.BitmapSequence")
                .FirstOrDefaultAsync(m => m.TerrainId == id);
            if (terrains == null)
            {
                return NotFound();
            }

            return View(terrains);
        }

        // GET: Terrain/Create
        public IActionResult Create()
        {
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x=>new { BitmapId=x.BitmapId, BitmapName=$"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View();
        }

        // POST: Terrain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerrainId,BitmapId,TerrainName")] Terrains terrains)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terrains);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View(terrains);
        }

        // GET: Terrain/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terrains = await _context.Terrains.FindAsync(id);
            if (terrains == null)
            {
                return NotFound();
            }
            ViewData["BitmapId"] = new SelectList(_context.Bitmaps.Include("BitmapSequence").Select(x => new { BitmapId = x.BitmapId, BitmapName = $"{x.BitmapSequence.BitmapSequenceName}#{x.BitmapIndex}" }), "BitmapId", "BitmapName");
            return View(terrains);
        }

        // POST: Terrain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TerrainId,BitmapId,TerrainName")] Terrains terrains)
        {
            if (id != terrains.TerrainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terrains);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerrainsExists(terrains.TerrainId))
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
            return View(terrains);
        }

        // GET: Terrain/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terrains = await _context.Terrains
                .Include(t => t.Bitmap)
                .FirstOrDefaultAsync(m => m.TerrainId == id);
            if (terrains == null)
            {
                return NotFound();
            }

            return View(terrains);
        }

        // POST: Terrain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terrains = await _context.Terrains.FindAsync(id);
            _context.Terrains.Remove(terrains);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerrainsExists(int id)
        {
            return _context.Terrains.Any(e => e.TerrainId == id);
        }
    }
}
