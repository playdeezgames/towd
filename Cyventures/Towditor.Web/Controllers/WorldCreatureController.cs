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
    public class WorldCreatureController : Controller
    {
        private readonly TOWDContext _context;

        public WorldCreatureController(TOWDContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> WorldIndex(int worldid)
        {
            var tOWDContext = _context.Worlds.Include("WorldCreatures.Creature.Bitmap.BitmapSequence").Where(x => x.WorldId == worldid);
            return View(await tOWDContext.SingleAsync());
        }
        public IActionResult AddToWorld(int worldid)
        {
            WorldCreatures model = new WorldCreatures() { WorldId = worldid };
            var availableCreatures =
                _context.Creatures.Include("WorldCreatures").Where(x => !x.WorldCreatures.Any(y => y.WorldId == worldid));
            ViewData["CreatureId"] = new SelectList(availableCreatures, "CreatureId", "CreatureName");
            ViewData["WorldId"] = new SelectList(_context.Worlds.Where(x => x.WorldId == worldid), "WorldId", "WorldName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWorld([Bind("WorldCreatureId,WorldId,CreatureId")] WorldCreatures worldCreatures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldCreatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(WorldIndex), new { worldid = worldCreatures.WorldId });
            }
            var availableCreatures =
                _context.Creatures.Include("WorldCreatures").Where(x => !x.WorldCreatures.Any(y => y.WorldId == worldCreatures.WorldId));
            ViewData["CreatureId"] = new SelectList(availableCreatures, "CreatureId", "CreatureName");
            ViewData["WorldId"] = new SelectList(_context.Worlds.Where(x => x.WorldId == worldCreatures.WorldId), "WorldId", "WorldName");
            return View(worldCreatures);
        }


        // GET: WorldCreature
        public async Task<IActionResult> Index()
        {
            var tOWDContext = _context.WorldCreatures.Include(w => w.Creature).Include(w => w.World);
            return View(await tOWDContext.ToListAsync());
        }

        // GET: WorldCreature/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCreatures = await _context.WorldCreatures
                .Include(w => w.Creature)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldCreatureId == id);
            if (worldCreatures == null)
            {
                return NotFound();
            }

            return View(worldCreatures);
        }

        // GET: WorldCreature/Create
        public IActionResult Create()
        {
            ViewData["CreatureId"] = new SelectList(_context.Creatures, "CreatureId", "CreatureName");
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName");
            return View();
        }

        // POST: WorldCreature/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorldCreatureId,WorldId,CreatureId")] WorldCreatures worldCreature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldCreature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatureId"] = new SelectList(_context.Creatures, "CreatureId", "CreatureName", worldCreature.CreatureId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldCreature.WorldId);
            return View(worldCreature);
        }

        // GET: WorldCreature/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCreatures = await _context.WorldCreatures.FindAsync(id);
            if (worldCreatures == null)
            {
                return NotFound();
            }
            ViewData["CreatureId"] = new SelectList(_context.Creatures, "CreatureId", "CreatureName", worldCreatures.CreatureId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldCreatures.WorldId);
            return View(worldCreatures);
        }

        // POST: WorldCreature/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorldCreatureId,WorldId,CreatureId")] WorldCreatures worldCreature)
        {
            if (id != worldCreature.WorldCreatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worldCreature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldCreaturesExists(worldCreature.WorldCreatureId))
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
            ViewData["CreatureId"] = new SelectList(_context.Creatures, "CreatureId", "CreatureName", worldCreature.CreatureId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldCreature.WorldId);
            return View(worldCreature);
        }

        // GET: WorldCreature/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCreatures = await _context.WorldCreatures
                .Include(w => w.Creature)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldCreatureId == id);
            if (worldCreatures == null)
            {
                return NotFound();
            }

            return View(worldCreatures);
        }

        // POST: WorldCreature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worldCreatures = await _context.WorldCreatures.FindAsync(id);
            _context.WorldCreatures.Remove(worldCreatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: WorldCreature/Delete/5
        public async Task<IActionResult> RemoveFromWorld(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCreatures = await _context.WorldCreatures
                .Include(w => w.Creature)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldCreatureId == id);
            if (worldCreatures == null)
            {
                return NotFound();
            }

            return View(worldCreatures);
        }

        // POST: WorldCreature/Delete/5
        [HttpPost, ActionName("RemoveFromWorld")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWorldConfirmed(int id)
        {
            var worldCreatures = await _context.WorldCreatures.FindAsync(id);
            var worldId = worldCreatures.WorldId;
            _context.WorldCreatures.Remove(worldCreatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorldIndex), new { worldid = worldId });
        }


        private bool WorldCreaturesExists(int id)
        {
            return _context.WorldCreatures.Any(e => e.WorldCreatureId == id);
        }
    }
}
