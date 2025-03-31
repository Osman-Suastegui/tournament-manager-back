using Microsoft.AspNetCore.Mvc;
using tournament_manager.DTOS.Tournament;
using tournament_manager.Services;

namespace tournament_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController:ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly TournamentService _tournamentService;

        public TournamentController(JwtService jwtService, TournamentService tournamentService)
        {
            _jwtService = jwtService;
            _tournamentService = tournamentService;
        }

        [HttpPost]
        public async Task <IActionResult> CreateTournament([FromBody] CreateTournamentDTO tournamentDto)
        {
            Console.WriteLine("test2");
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized("No token provided");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var userId = _jwtService.ExtractUserId(token);

            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }

            await _tournamentService.createTournament(tournamentDto,userId);
            return Ok("Tournament created");
        }
    }
}
