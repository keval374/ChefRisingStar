using LTDCWebservice.Authentication;
using LTDCWebservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LTDCWebservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _authenticationManager;
        public AuthController(IJWTAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
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
    }
}
