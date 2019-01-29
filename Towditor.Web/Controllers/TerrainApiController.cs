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
    public class TerrainApiController : ControllerBase
    {
        private readonly TOWDContext _context;

        public TerrainApiController(TOWDContext context)
        {
            _context = context;
        }

        // GET: api/TerrainApi
        [HttpGet]
        public ActionResult<IEnumerable<Models.TerrainModel>> GetTerrains()
        {
            return _context.Terrains.Select(Models.TerrainModel.FromTerrain).ToList();
        }

        // GET: api/TerrainApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Terrains>> GetTerrains(int id)
        {
            var terrains = await _context.Terrains.FindAsync(id);

            if (terrains == null)
            {
                return NotFound();
            }

            return terrains;
        }

        // PUT: api/TerrainApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerrains(int id, Terrains terrains)
        {
            if (id != terrains.TerrainId)
            {
                return BadRequest();
            }

            _context.Entry(terrains).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerrainsExists(id))
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

        // POST: api/TerrainApi
        [HttpPost]
        public async Task<ActionResult<Terrains>> PostTerrains(Terrains terrains)
        {
            _context.Terrains.Add(terrains);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTerrains", new { id = terrains.TerrainId }, terrains);
        }

        // DELETE: api/TerrainApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Terrains>> DeleteTerrains(int id)
        {
            var terrains = await _context.Terrains.FindAsync(id);
            if (terrains == null)
            {
                return NotFound();
            }

            _context.Terrains.Remove(terrains);
            await _context.SaveChangesAsync();

            return terrains;
        }

        private bool TerrainsExists(int id)
        {
            return _context.Terrains.Any(e => e.TerrainId == id);
        }
    }
}
