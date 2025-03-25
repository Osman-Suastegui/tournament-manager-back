using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tournament_manager.DTOS.Users;
using tournament_manager.Services;

namespace tournament_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly JwtService _jwtService;
        private readonly UserService _userService;
        public UsersController(JwtService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<string>> getUsers()
        {
            List<string> wordList = ["FIRST", "second22"];
            return Ok(wordList);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(string user)
        {

            string token = _jwtService.GenerateToken("123", "osman@hotmail.com");

            //await _userService.AddUserAsync(user);
            Console.WriteLine("test");
            return token;
        }



        [AllowAnonymous] // 👈 This is the key!

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest user)
        {
            var token = await _userService.Login(user);

            if (token == null)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(new { token });

        }
    }
}
