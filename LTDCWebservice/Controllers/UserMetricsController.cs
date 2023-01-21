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
    public class UserMetricsController : ControllerBase
    {
        private readonly LtdcContext _context;

        public UserMetricsController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/UserMetrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMetric>>> GetUserMetrics()
        {
            return await _context.UserMetrics.ToListAsync();
        }

        // GET: api/UserMetrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMetric>> GetUserMetric(long id)
        {
            var userMetric = await _context.UserMetrics.FindAsync(id);

            if (userMetric == null)
            {
                return NotFound();
            }

            return userMetric;
        }

        // PUT: api/UserMetrics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMetric(long id, UserMetric userMetric)
        {
            if (id != userMetric.Id)
            {
                return BadRequest();
            }

            _context.Entry(userMetric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMetricExists(id))
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

        // POST: api/UserMetrics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMetric>> PostUserMetric(UserMetric userMetric)
        {
            _context.UserMetrics.Add(userMetric);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserMetric", new { id = userMetric.Id }, userMetric);
        }

        // DELETE: api/UserMetrics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMetric(long id)
        {
            var userMetric = await _context.UserMetrics.FindAsync(id);
            if (userMetric == null)
            {
                return NotFound();
            }

            _context.UserMetrics.Remove(userMetric);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserMetricExists(long id)
        {
            return _context.UserMetrics.Any(e => e.Id == id);
        }
    }
}
