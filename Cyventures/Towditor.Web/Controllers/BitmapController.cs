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
            var ctx = _context.Bitmaps.Include(b => b.BitmapSequence).OrderBy(x=>x.BitmapSequence.BitmapSequenceName).ThenBy(x=>x.BitmapIndex);
            return View(await ctx.ToListAsync());
        }

        // GET: Bitmap/Details/5
        public async Task<IActionResult> Details(int? id, int? parentid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmap = await _context.Bitmaps
                .Include(b => b.BitmapSequence)
                .FirstOrDefaultAsync(m => m.BitmapId == id);
            if (bitmap == null)
            {
                return NotFound();
            }
            if(parentid.HasValue)
            {
                ViewData["ParentId"] = parentid.Value;
            }
            return View(bitmap);
        }

        // GET: Bitmap/Create
        public IActionResult Create(int? parentid)
        {
            if (parentid.HasValue)
            {
                ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.Where(x=>x.BitmapSequenceId==parentid.Value), "BitmapSequenceId", "BitmapSequenceName");
                ViewData["ParentId"] = parentid.Value;
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
        public async Task<IActionResult> Create([Bind("BitmapId,BitmapWidth,BitmapHeight,BitmapSequenceId,BitmapIndex")] Bitmaps bitmap, int? parentid)
        {
            bitmap.BitmapIndex = _context.Bitmaps.Where(x => x.BitmapSequenceId == bitmap.BitmapSequenceId).Count();
            if (ModelState.IsValid)
            {
                _context.Add(bitmap);
                await _context.SaveChangesAsync();
                if (parentid.HasValue)
                {
                    return RedirectToAction("Details", "BitmapSequence", new { id = parentid.Value });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmap.BitmapSequenceId);
            return View(bitmap);
        }

        // GET: Bitmap/Edit/5
        public async Task<IActionResult> Edit(int? id, int? parentid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmap = await _context.Bitmaps.FindAsync(id);
            if (bitmap == null)
            {
                return NotFound();
            }
            if (parentid.HasValue)
            {
                ViewData["ParentId"] = parentid.Value;
            }
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmap.BitmapSequenceId);
            return View(bitmap);
        }

        // POST: Bitmap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BitmapId,BitmapWidth,BitmapHeight,BitmapSequenceId,BitmapIndex")] Bitmaps bitmap, int? parentid)
        {
            if (id != bitmap.BitmapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldBitmap = await _context.Bitmaps.FindAsync(id);
                    oldBitmap.BitmapHeight = bitmap.BitmapHeight;
                    oldBitmap.BitmapWidth = bitmap.BitmapWidth;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BitmapsExists(bitmap.BitmapId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (parentid.HasValue)
                {
                    return RedirectToAction("Details", "BitmapSequence", new { id = parentid.Value });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["BitmapSequenceId"] = new SelectList(_context.BitmapSequences.OrderBy(x => x.BitmapSequenceName), "BitmapSequenceId", "BitmapSequenceName", bitmap.BitmapSequenceId);
            return View(bitmap);
        }

        // GET: Bitmap/Delete/5
        public async Task<IActionResult> Delete(int? id, int? parentid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmap = await _context.Bitmaps
                .Include(b => b.BitmapSequence)
                .FirstOrDefaultAsync(m => m.BitmapId == id);
            if (bitmap == null)
            {
                return NotFound();
            }
            if (parentid.HasValue)
            {
                ViewData["ParentId"] = parentid.Value;
            }
            return View(bitmap);
        }

        // POST: Bitmap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? parentid)
        {
            var bitmapPixels = _context.BitmapPixels.Where(x => x.BitmapId == id);
            _context.BitmapPixels.RemoveRange(bitmapPixels);
            var bitmap = await _context.Bitmaps.FindAsync(id);
            var bitmapSequenceId = bitmap.BitmapSequenceId;
            var bitmapIndex = bitmap.BitmapIndex;
            _context.Bitmaps.Remove(bitmap);
            await _context.SaveChangesAsync();
            var bitmaps = _context.Bitmaps.Where(x => x.BitmapSequenceId == bitmapSequenceId && x.BitmapIndex > bitmapIndex);
            foreach(var x in bitmaps)
            {
                x.BitmapIndex--;
            }
            await _context.SaveChangesAsync();
            if (parentid.HasValue)
            {
                return RedirectToAction("Details", "BitmapSequence", new { id = parentid.Value });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool BitmapsExists(int id)
        {
            return _context.Bitmaps.Any(e => e.BitmapId == id);
        }
    }
}
