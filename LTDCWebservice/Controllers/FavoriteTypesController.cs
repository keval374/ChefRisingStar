using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTDCWebservice.Models;
using Microsoft.AspNetCore.Authorization;

namespace LTDCWebservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteTypesController : ControllerBase
    {
        private readonly LtdcContext _context;

        public FavoriteTypesController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteType>>> GetFavoriteTypes()
        {
            return await _context.FavoriteTypes.ToListAsync();
        }

        // GET: api/FavoriteTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteType>> GetFavoriteType(long id)
        {
            var favoriteType = await _context.FavoriteTypes.FindAsync(id);

            if (favoriteType == null)
            {
                return NotFound();
            }

            return favoriteType;
        }

        // PUT: api/FavoriteTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteType(long id, FavoriteType favoriteType)
        {
            if (id != favoriteType.Id)
            {
                return BadRequest();
            }

            _context.Entry(favoriteType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteTypeExists(id))
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

        // POST: api/FavoriteTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteType>> PostFavoriteType(FavoriteType favoriteType)
        {
            _context.FavoriteTypes.Add(favoriteType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteType", new { id = favoriteType.Id }, favoriteType);
        }

        // DELETE: api/FavoriteTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteType(long id)
        {
            var favoriteType = await _context.FavoriteTypes.FindAsync(id);
            if (favoriteType == null)
            {
                return NotFound();
            }

            _context.FavoriteTypes.Remove(favoriteType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteTypeExists(long id)
        {
            return _context.FavoriteTypes.Any(e => e.Id == id);
        }
    }
}
