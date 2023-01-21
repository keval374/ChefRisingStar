using LTDCWebservice.Authentication;
using LTDCWebservice.Models;
using LTDCWebservice.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        
        // GET: api/<NameController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
            var FoundUser = _context.Users.Any(u=>u.Email == user.Email || u.UserName == user.UserName);

            if (FoundUser)
            {
                return BadRequest();
            }

            byte[] salt;

            string hash = HashUtility.HashPasword(user.PasswordHash, out salt);
            string hash2 = HashUtility.HashPaswordWithSalt(user.PasswordHash, salt);
            return Ok(Convert.ToHexString(salt));
        }
        
        [AllowAnonymous]
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
