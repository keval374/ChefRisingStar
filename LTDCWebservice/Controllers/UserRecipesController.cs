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
    public class UserRecipesController : ControllerBase
    {
        private readonly LtdcContext _context;

        public UserRecipesController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/UserRecipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRecipe>>> GetUserRecipes()
        {
            return await _context.UserRecipes.ToListAsync();
        }

        // GET: api/UserRecipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRecipe>> GetUserRecipe(long id)
        {
            var userRecipe = await _context.UserRecipes.FindAsync(id);

            if (userRecipe == null)
            {
                return NotFound();
            }

            return userRecipe;
        }

        // PUT: api/UserRecipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRecipe(long id, UserRecipe userRecipe)
        {
            if (id != userRecipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(userRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRecipeExists(id))
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

        // POST: api/UserRecipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRecipe>> PostUserRecipe(UserRecipe userRecipe)
        {
            _context.UserRecipes.Add(userRecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRecipe", new { id = userRecipe.Id }, userRecipe);
        }

        // DELETE: api/UserRecipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRecipe(long id)
        {
            var userRecipe = await _context.UserRecipes.FindAsync(id);
            if (userRecipe == null)
            {
                return NotFound();
            }

            _context.UserRecipes.Remove(userRecipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRecipeExists(long id)
        {
            return _context.UserRecipes.Any(e => e.Id == id);
        }
    }
}
