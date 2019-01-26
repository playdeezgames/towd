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
    public class RoomController : Controller
    {
        private readonly TOWDContext _context;

        public RoomController(TOWDContext context)
        {
            _context = context;
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.World)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // GET: Room/Create
        public IActionResult Create(int worldid)
        {
            Models.RoomCreationModel room = new Models.RoomCreationModel()
            {
                WorldId=worldid,
                Columns=12,
                Rows=12
            };
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName");
            return View(room);
        }

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomName,WorldId,Columns,Rows,TerrainId")] Models.RoomCreationModel model)
        {
            if (ModelState.IsValid)
            {
                Rooms room = new Rooms()
                {
                    WorldId=model.WorldId,
                    RoomName=model.RoomName
                };
                List<Tiles> tiles = new List<Tiles>();
                for(int column=0;column<model.Columns;++column)
                {
                    for(int row=0;row<model.Rows;++row)
                    {
                        tiles.Add(new Tiles() { TerrainId = model.TerrainId, X = column, Y = row });
                    }
                }
                room.Tiles = tiles;
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","World",new { id = room.WorldId });
            }
            ViewData["TerrainId"] = new SelectList(_context.Terrains, "TerrainId", "TerrainName");
            return View(model);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }
            return View(rooms);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomName,WorldId")] Rooms room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(room.RoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "World", new { id = room.WorldId });
            }
            return View(room);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.World)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            var tiles = _context.Tiles.Where(x => x.RoomId == id);
            var worldId = room.WorldId;
            _context.Tiles.RemoveRange(tiles);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "World", new { id = worldId });
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
