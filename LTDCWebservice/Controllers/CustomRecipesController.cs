using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTDCWebservice.Models;
using Microsoft.AspNetCore.Authorization;
using LTDCWebservice.Utilities;
using LTDCWebservice.Handlers;

namespace LTDCWebservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomRecipesController : ControllerBase
    {
        private readonly LtdcContext _context;

        public CustomRecipesController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/CustomRecipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomRecipe>>> GetCustomRecipe()
        {
            return await _context.CustomRecipes.ToListAsync();
        }

        // GET: api/CustomRecipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomRecipe>> GetCustomRecipe(long id)
        {
            var customrecipe = await _context.CustomRecipes.FindAsync(id);

            if (customrecipe == null)
            {
                return NotFound();
            }

            return customrecipe;
        }

        // PUT: api/CustomRecipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomRecipe(long id, CustomRecipe customrecipe)
        {
            if (id != customrecipe.ID)
            {
                return BadRequest();
            }

            
            _context.Entry(customrecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomRecipeExists(id))
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

        // POST: api/CustomRecipes
        [HttpPost]
        public async Task<ActionResult<CustomRecipe>> PostCustomRecipe(CustomRecipe customrecipe)
        {
            CustomRecipeHandler handler = new CustomRecipeHandler(_context);
            var result = handler.CreateAccount(customrecipe);

            if (result is OkObjectResult)
            {
                return CreatedAtAction("GetCustomRecipe", new { id = customrecipe.Id }, customrecipe);
            }

            return NoContent();
        }

        // DELETE: api/CustomRecipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomRecipe(long id)
        {
            var customrecipe = await _context.CustomRecipes.FindAsync(id);
            if (customrecipe == null)
            {
                return NotFound();
            }

            _context.CustomRecipes.Remove(customrecipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomRecipeExists(long id)
        {
            return _context.CustomRecipes.Any(e => e.ID == id);
        }
    }
}
