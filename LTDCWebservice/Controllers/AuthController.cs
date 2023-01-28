using LTDCWebservice.Authentication;
using LTDCWebservice.Handlers;
using LTDCWebservice.Models;
using LTDCWebservice.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LTDCWebservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _authenticationManager;

        private readonly LtdcContext _context;
               
        public AuthController(IJWTAuthenticationManager authenticationManager, LtdcContext context)
        {
            _authenticationManager = authenticationManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _authenticationManager.Authenticate(userCred.Username, userCred.Password);

            if(token == null)
                return Unauthorized();

            return Ok(token);
        }
        
        [AllowAnonymous]
        [HttpPost("createAccount")]
        public IActionResult CreateAccount([FromBody] User user)
        {
            UserHandler handler = new UserHandler(_context);
            return handler.CreateAccount(user);
        }
        
        [AllowAnonymous]
        [HttpPost("resetAccount")]
        public IActionResult ResetAccount([FromBody] UserCred userCred)
        {
            var foundUser = _context.Users.FirstOrDefault(u=>u.Email == userCred.Username || u.UserName == userCred.Username);

            if (foundUser == null)
            {
                return BadRequest();
            }

            string salt;
            string hash = HashUtility.HashPasword(userCred.Password, out salt);

            var result = new
            {
                PasswordHash = hash,
                Salt = salt
            };

            _context.Entry(foundUser).State = EntityState.Modified;
            foundUser.PasswordHash = hash;
            foundUser.Salt = salt;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("getNounce")]
        public IActionResult GetNounce()
        {
            byte[] previousNonce = null;
            
            if (Request.Cookies.TryGetValue("userNonce", out string value))
            {
                previousNonce = ASCIIEncoding.ASCII.GetBytes(value);
                return Ok(previousNonce);
            }

            byte[] bytes = new byte[64];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetNonZeroBytes(bytes);

            string encodedBytes = ASCIIEncoding.ASCII.GetString(bytes);
            Response.Cookies.Append("userNonce", encodedBytes);

            return Ok(bytes);
        }
    }
}
