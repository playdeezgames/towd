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
    public class FontController : Controller
    {
        private readonly TOWDContext _context;

        public FontController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Font
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fonts.ToListAsync());
        }

        // GET: Font/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var font = await _context.Fonts
                .FirstOrDefaultAsync(m => m.FontId == id);
            if (font == null)
            {
                return NotFound();
            }

            return View(font);
        }

        // GET: Font/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Font/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FontId,FontName,FontHeight")] Fonts font)
        {
            if (ModelState.IsValid)
            {
                _context.Add(font);
                for (int index = 32; index < 128; ++index)
                {
                    _context.Add(new EFModel.Glyphs()
                    {
                        Font=font,
                        GlyphWidth=1,
                        GlyphCharacter=index
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(font);
        }

        // GET: Font/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var font = await _context.Fonts.FindAsync(id);
            if (font == null)
            {
                return NotFound();
            }
            return View(font);
        }

        // POST: Font/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FontId,FontName,FontHeight")] Fonts font)
        {
            if (id != font.FontId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(font);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FontsExists(font.FontId))
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
            return View(font);
        }

        // GET: Font/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var font = await _context.Fonts
                .FirstOrDefaultAsync(m => m.FontId == id);
            if (font == null)
            {
                return NotFound();
            }

            return View(font);
        }

        // POST: Font/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var font = await _context.Fonts.FindAsync(id);
            var glyphPixels = _context.GlyphPixels.Include("Glyph").Where(x => x.Glyph.FontId == id);
            var glyphs = _context.Glyphs.Where(x => x.FontId == id);
            _context.GlyphPixels.RemoveRange(glyphPixels);
            _context.Glyphs.RemoveRange(glyphs);
            _context.Fonts.Remove(font);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FontsExists(int id)
        {
            return _context.Fonts.Any(e => e.FontId == id);
        }
    }
}
