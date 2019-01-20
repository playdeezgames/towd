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
    public class BitmapSequenceController : Controller
    {
        private readonly TOWDContext _context;

        public BitmapSequenceController(TOWDContext context)
        {
            _context = context;
        }

        // GET: BitmapSequence
        public async Task<IActionResult> Index()
        {
            return View(await _context.BitmapSequences.Include("Bitmaps").ToListAsync());
        }

        // GET: BitmapSequence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmapSequences = await _context.BitmapSequences.Include("Bitmaps")
                .FirstOrDefaultAsync(m => m.BitmapSequenceId == id);
            if (bitmapSequences == null)
            {
                return NotFound();
            }

            return View(bitmapSequences);
        }

        // GET: BitmapSequence/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BitmapSequence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BitmapSequenceId,BitmapSequenceName")] BitmapSequences bitmapSequences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bitmapSequences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bitmapSequences);
        }

        // GET: BitmapSequence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmapSequences = await _context.BitmapSequences.FindAsync(id);
            if (bitmapSequences == null)
            {
                return NotFound();
            }
            return View(bitmapSequences);
        }

        // POST: BitmapSequence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BitmapSequenceId,BitmapSequenceName")] BitmapSequences bitmapSequences)
        {
            if (id != bitmapSequences.BitmapSequenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bitmapSequences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BitmapSequencesExists(bitmapSequences.BitmapSequenceId))
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
            return View(bitmapSequences);
        }

        // GET: BitmapSequence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bitmapSequences = await _context.BitmapSequences
                .FirstOrDefaultAsync(m => m.BitmapSequenceId == id);
            if (bitmapSequences == null)
            {
                return NotFound();
            }

            return View(bitmapSequences);
        }

        // POST: BitmapSequence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bitmapSequences = await _context.BitmapSequences.FindAsync(id);
            _context.BitmapSequences.Remove(bitmapSequences);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BitmapSequencesExists(int id)
        {
            return _context.BitmapSequences.Any(e => e.BitmapSequenceId == id);
        }
    }
}
