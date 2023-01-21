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
    public class UserAchievementStepsController : ControllerBase
    {
        private readonly LtdcContext _context;

        public UserAchievementStepsController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/UserAchievementSteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAchievementStep>>> GetUserAchievementSteps()
        {
            return await _context.UserAchievementSteps.ToListAsync();
        }

        // GET: api/UserAchievementSteps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAchievementStep>> GetUserAchievementStep(long id)
        {
            var userAchievementStep = await _context.UserAchievementSteps.FindAsync(id);

            if (userAchievementStep == null)
            {
                return NotFound();
            }

            return userAchievementStep;
        }

        // PUT: api/UserAchievementSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAchievementStep(long id, UserAchievementStep userAchievementStep)
        {
            if (id != userAchievementStep.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userAchievementStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAchievementStepExists(id))
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

        // POST: api/UserAchievementSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAchievementStep>> PostUserAchievementStep(UserAchievementStep userAchievementStep)
        {
            _context.UserAchievementSteps.Add(userAchievementStep);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAchievementStepExists(userAchievementStep.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAchievementStep", new { id = userAchievementStep.UserId }, userAchievementStep);
        }

        // DELETE: api/UserAchievementSteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAchievementStep(long id)
        {
            var userAchievementStep = await _context.UserAchievementSteps.FindAsync(id);
            if (userAchievementStep == null)
            {
                return NotFound();
            }

            _context.UserAchievementSteps.Remove(userAchievementStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAchievementStepExists(long id)
        {
            return _context.UserAchievementSteps.Any(e => e.UserId == id);
        }
    }
}
