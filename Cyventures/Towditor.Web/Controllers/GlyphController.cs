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
    public class GlyphController : Controller
    {
        private readonly TOWDContext _context;

        public GlyphController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Glyph
        public async Task<IActionResult> Index()
        {
            var ctx = _context.Glyphs.Include(g => g.Font).OrderBy(x=>x.Font.FontName).ThenBy(x=>x.GlyphCharacter);
            return View(await ctx.ToListAsync());
        }

        // GET: Glyph/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glyphs = await _context.Glyphs
                .Include(g => g.Font)
                .FirstOrDefaultAsync(m => m.GlyphId == id);
            if (glyphs == null)
            {
                return NotFound();
            }

            return View(glyphs);
        }

        // GET: Glyph/Create
        public IActionResult Create()
        {
            ViewData["FontId"] = new SelectList(_context.Fonts, "FontId", "FontName");
            return View();
        }

        // POST: Glyph/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GlyphId,GlyphCharacter,FontId,GlyphWidth")] Glyphs glyphs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glyphs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FontId"] = new SelectList(_context.Fonts, "FontId", "FontName", glyphs.FontId);
            return View(glyphs);
        }

        // GET: Glyph/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glyphs = await _context.Glyphs.FindAsync(id);
            if (glyphs == null)
            {
                return NotFound();
            }
            ViewData["FontId"] = new SelectList(_context.Fonts, "FontId", "FontName", glyphs.FontId);
            return View(glyphs);
        }

        // POST: Glyph/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GlyphId,GlyphCharacter,FontId,GlyphWidth")] Glyphs glyphs)
        {
            if (id != glyphs.GlyphId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glyphs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlyphsExists(glyphs.GlyphId))
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
            ViewData["FontId"] = new SelectList(_context.Fonts, "FontId", "FontName", glyphs.FontId);
            return View(glyphs);
        }

        // GET: Glyph/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glyphs = await _context.Glyphs
                .Include(g => g.Font)
                .FirstOrDefaultAsync(m => m.GlyphId == id);
            if (glyphs == null)
            {
                return NotFound();
            }

            return View(glyphs);
        }

        // POST: Glyph/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glyphs = await _context.Glyphs.FindAsync(id);
            _context.Glyphs.Remove(glyphs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlyphsExists(int id)
        {
            return _context.Glyphs.Any(e => e.GlyphId == id);
        }
    }
}
