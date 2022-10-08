using Log_In.API.Data;
using Log_In.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Log_In.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly MongoCrud db;

        public UserController(MongoCrud db)
        {
            this.db = db;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok("Welcome");
        }

        [HttpPost("CreateUser")]
        public IActionResult Create(LoginCredential credential)
        {
            return db.CreateUser(credential) ? Ok() : BadRequest("User with email already exists"); 
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginCredential credential)
        {
            string? sessionToken = db.Login(credential);
            return sessionToken == null ? BadRequest("Nope") : Ok(sessionToken);
        }

        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] string sessionToken)
        {
            db.Logout(sessionToken);
            return Ok();
        }
    }
}
