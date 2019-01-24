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
    public class WorldTerrainController : Controller
    {
        private readonly TOWDContext _context;

        public WorldTerrainController(TOWDContext context)
        {
            _context = context;
        }

        // GET: WorldTerrain
        public async Task<IActionResult> Index()
        {
            var tOWDContext = _context.WorldTerrains.Include(w => w.Terrain).Include(w => w.World);
            return View(await tOWDContext.ToListAsync());
        }

        public async Task<IActionResult> WorldIndex(int worldid)
        {
            var tOWDContext = _context.Worlds.Include("WorldTerrains.Terrain.Bitmap.BitmapSequence").Where(x=>x.WorldId== worldid);
            return View(await tOWDContext.SingleAsync());
        }
        public IActionResult AddToWorld(int worldid)
        {
            WorldTerrains model = new WorldTerrains() { WorldId=worldid };
            var availableTerrains = 
                _context.Terrains.Include("WorldTerrains").Where(x=>!x.WorldTerrains.Any(y=>y.WorldId==worldid));
            ViewData["TerrainId"] = new SelectList(availableTerrains, "TerrainId", "TerrainName");
            ViewData["WorldId"] = new SelectList(_context.Worlds.Where(x=>x.WorldId==worldid), "WorldId", "WorldName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWorld([Bind("WorldTerrainId,WorldId,TerrainId")] WorldTerrains worldTerrains)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldTerrains);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(WorldIndex),new { worldid=worldTerrains.WorldId });
            }
            var availableTerrains =
                _context.Terrains.Include("WorldTerrains").Where(x => !x.WorldTerrains.Any(y => y.WorldId == worldTerrains.WorldId));
            ViewData["TerrainId"] = new SelectList(availableTerrains, "TerrainId", "TerrainName");
            ViewData["WorldId"] = new SelectList(_context.Worlds.Where(x => x.WorldId == worldTerrains.WorldId), "WorldId", "WorldName");
            return View(worldTerrains);
        }

        // GET: WorldTerrain/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldTerrains = await _context.WorldTerrains
                .Include(w => w.Terrain)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldTerrainId == id);
            if (worldTerrains == null)
            {
                return NotFound();
            }

            return View(worldTerrains);
        }

        // GET: WorldTerrain/Create
        public IActionResult Create()
        {
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName");
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName");
            return View();
        }

        // POST: WorldTerrain/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorldTerrainId,WorldId,TerrainId")] WorldTerrains worldTerrains)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldTerrains);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName", worldTerrains.TerrainId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldTerrains.WorldId);
            return View(worldTerrains);
        }

        // GET: WorldTerrain/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldTerrains = await _context.WorldTerrains.FindAsync(id);
            if (worldTerrains == null)
            {
                return NotFound();
            }
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName", worldTerrains.TerrainId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldTerrains.WorldId);
            return View(worldTerrains);
        }

        // POST: WorldTerrain/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorldTerrainId,WorldId,TerrainId")] WorldTerrains worldTerrains)
        {
            if (id != worldTerrains.WorldTerrainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worldTerrains);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldTerrainsExists(worldTerrains.WorldTerrainId))
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
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName", worldTerrains.TerrainId);
            ViewData["WorldId"] = new SelectList(_context.Worlds, "WorldId", "WorldName", worldTerrains.WorldId);
            return View(worldTerrains);
        }

        // GET: WorldTerrain/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldTerrains = await _context.WorldTerrains
                .Include(w => w.Terrain)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldTerrainId == id);
            if (worldTerrains == null)
            {
                return NotFound();
            }

            return View(worldTerrains);
        }

        // POST: WorldTerrain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worldTerrains = await _context.WorldTerrains.FindAsync(id);
            _context.WorldTerrains.Remove(worldTerrains);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WorldTerrain/Delete/5
        public async Task<IActionResult> RemoveFromWorld(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldTerrains = await _context.WorldTerrains
                .Include(w => w.Terrain)
                .Include(w => w.World)
                .FirstOrDefaultAsync(m => m.WorldTerrainId == id);
            if (worldTerrains == null)
            {
                return NotFound();
            }

            return View(worldTerrains);
        }

        // POST: WorldTerrain/Delete/5
        [HttpPost, ActionName("RemoveFromWorld")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWorldConfirmed(int id)
        {
            var worldTerrains = await _context.WorldTerrains.FindAsync(id);
            var worldId = worldTerrains.WorldId;
            _context.WorldTerrains.Remove(worldTerrains);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorldIndex), new { worldid = worldId});
        }

        private bool WorldTerrainsExists(int id)
        {
            return _context.WorldTerrains.Any(e => e.WorldTerrainId == id);
        }
    }
}
