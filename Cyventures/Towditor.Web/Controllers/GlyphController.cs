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
        public async Task<IActionResult> Details(int? id, int? parentid)
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

        // GET: Glyph/Edit/5
        public async Task<IActionResult> Edit(int? id, int? parentid)
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
        public async Task<IActionResult> Edit(int id, [Bind("GlyphId,GlyphCharacter,FontId,GlyphWidth")] Glyphs glyph, int? parentid)
        {
            if (id != glyph.GlyphId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glyph);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlyphsExists(glyph.GlyphId))
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
            ViewData["FontId"] = new SelectList(_context.Fonts, "FontId", "FontName", glyph.FontId);
            return View(glyph);
        }

        private bool GlyphsExists(int id)
        {
            return _context.Glyphs.Any(e => e.GlyphId == id);
        }
    }
}
