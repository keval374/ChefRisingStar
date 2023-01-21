﻿using System;
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
    public class UserAchievementsController : ControllerBase
    {
        private readonly LtdcContext _context;

        public UserAchievementsController(LtdcContext context)
        {
            _context = context;
        }

        // GET: api/UserAchievements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAchievement>>> GetUserAchievements()
        {
            return await _context.UserAchievements.ToListAsync();
        }

        // GET: api/UserAchievements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAchievement>> GetUserAchievement(long id)
        {
            var userAchievement = await _context.UserAchievements.FindAsync(id);

            if (userAchievement == null)
            {
                return NotFound();
            }

            return userAchievement;
        }

        // PUT: api/UserAchievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAchievement(long id, UserAchievement userAchievement)
        {
            if (id != userAchievement.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userAchievement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAchievementExists(id))
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

        // POST: api/UserAchievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAchievement>> PostUserAchievement(UserAchievement userAchievement)
        {
            _context.UserAchievements.Add(userAchievement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAchievementExists(userAchievement.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAchievement", new { id = userAchievement.UserId }, userAchievement);
        }

        // DELETE: api/UserAchievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAchievement(long id)
        {
            var userAchievement = await _context.UserAchievements.FindAsync(id);
            if (userAchievement == null)
            {
                return NotFound();
            }

            _context.UserAchievements.Remove(userAchievement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAchievementExists(long id)
        {
            return _context.UserAchievements.Any(e => e.UserId == id);
        }
    }
}
