using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Towditor.Web.EFModel;

namespace Towditor.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomApiController : ControllerBase
    {
        private readonly TOWDContext _context;

        public RoomApiController(TOWDContext context)
        {
            _context = context;
        }

        // GET: api/RoomApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rooms>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/RoomApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.RoomEditorRoomModel>> GetRoom(int id)
        {
            var room = await _context.Rooms.Include("Tiles").SingleOrDefaultAsync(x=>x.RoomId==id);

            if (room == null)
            {
                return NotFound();
            }

            return Models.RoomEditorRoomModel.FromRoom(room);
        }

        // PUT: api/RoomApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Rooms room)
        {
            if (id != room.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomApi
        [HttpPost]
        public async Task<ActionResult<Rooms>> PostRoom(Rooms room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = room.RoomId }, room);
        }

        // DELETE: api/RoomApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rooms>> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
