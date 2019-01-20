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

            var fonts = await _context.Fonts
                .FirstOrDefaultAsync(m => m.FontId == id);
            if (fonts == null)
            {
                return NotFound();
            }

            return View(fonts);
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
        public async Task<IActionResult> Create([Bind("FontId,FontName,FontHeight")] Fonts fonts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fonts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fonts);
        }

        // GET: Font/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fonts = await _context.Fonts.FindAsync(id);
            if (fonts == null)
            {
                return NotFound();
            }
            return View(fonts);
        }

        // POST: Font/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FontId,FontName,FontHeight")] Fonts fonts)
        {
            if (id != fonts.FontId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fonts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FontsExists(fonts.FontId))
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
            return View(fonts);
        }

        // GET: Font/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fonts = await _context.Fonts
                .FirstOrDefaultAsync(m => m.FontId == id);
            if (fonts == null)
            {
                return NotFound();
            }

            return View(fonts);
        }

        // POST: Font/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fonts = await _context.Fonts.FindAsync(id);
            _context.Fonts.Remove(fonts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FontsExists(int id)
        {
            return _context.Fonts.Any(e => e.FontId == id);
        }
    }
}
