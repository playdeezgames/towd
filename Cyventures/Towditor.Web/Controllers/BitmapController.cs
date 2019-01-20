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
    public class BitmapController : Controller
    {
        private readonly TOWDContext _context;

        public BitmapController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Bitmap
        public async Task<IActionResult> Index()
        {
            var tOWDContext = _context.Bitmaps.Include(b => b.BitmapSequence).OrderBy(x=>x.BitmapSequence.BitmapSequenceName).ThenBy(x=>x.BitmapIndex);
            return View(await tOWDContext.ToListAsync());
        }

        // GET: Bitmap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmaps = await _context.Bitmaps
                .Include(b => b.BitmapSequence)
                .FirstOrDefaultAsync(m => m.BitmapId == id);
            if (bitmaps == null)
            {
                return NotFound();
            }

            return View(bitmaps);
        }

        // GET: Bitmap/Create
        public IActionResult Create(int? parentid)
        {
            if (parentid.HasValue)
            {
                ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.Where(x=>x.BitmapSequenceId==parentid.Value), "BitmapSequenceId", "BitmapSequenceName");
            }
            else
            {
                ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName");
            }
            return View();
        }

        // POST: Bitmap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BitmapId,BitmapWidth,BitmapHeight,BitmapSequenceId,BitmapIndex")] Bitmaps bitmaps)
        {
            bitmaps.BitmapIndex = _context.Bitmaps.Where(x => x.BitmapSequenceId == bitmaps.BitmapSequenceId).Count();
            if (ModelState.IsValid)
            {
                _context.Add(bitmaps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmaps.BitmapSequenceId);
            return View(bitmaps);
        }

        // GET: Bitmap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmaps = await _context.Bitmaps.FindAsync(id);
            if (bitmaps == null)
            {
                return NotFound();
            }
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmaps.BitmapSequenceId);
            return View(bitmaps);
        }

        // POST: Bitmap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BitmapId,BitmapWidth,BitmapHeight,BitmapSequenceId,BitmapIndex")] Bitmaps bitmaps)
        {
            if (id != bitmaps.BitmapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bitmaps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BitmapsExists(bitmaps.BitmapId))
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
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmaps.BitmapSequenceId);
            return View(bitmaps);
        }

        // GET: Bitmap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmaps = await _context.Bitmaps
                .Include(b => b.BitmapSequence)
                .FirstOrDefaultAsync(m => m.BitmapId == id);
            if (bitmaps == null)
            {
                return NotFound();
            }

            return View(bitmaps);
        }

        // POST: Bitmap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bitmaps = await _context.Bitmaps.FindAsync(id);
            _context.Bitmaps.Remove(bitmaps);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BitmapsExists(int id)
        {
            return _context.Bitmaps.Any(e => e.BitmapId == id);
        }
    }
}
