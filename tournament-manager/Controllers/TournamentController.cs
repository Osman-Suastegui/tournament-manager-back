using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tournament_manager.DTOS.Tournament;
using tournament_manager.Models;
using tournament_manager.Services;

namespace tournament_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly TournamentService _tournamentService;

        public TournamentController(JwtService jwtService, TournamentService tournamentService)
        {
            _jwtService = jwtService;
            _tournamentService = tournamentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDTO tournamentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("User ID: " + userId);

            TournamentDTO response = await _tournamentService.createTournament(tournamentDto, userId);
            return CreatedAtAction(
                nameof(GetTournamentById),      // action that retrieves a single tournament
                new { id = response.Id },     // route values so the client can call it
                response);
        }

        [HttpGet]
        public async Task<List<Tournament>> GetTournaments([FromQuery] string? q,[FromQuery] int limit,[FromQuery] int skip,[FromQuery] string? status)
        {
            return await _tournamentService.getTournaments(q, limit, skip,status);
        }

        [HttpGet("getTournamentById")]
        public async Task<Tournament> GetTournamentById([FromQuery] long id)
        {

            return await _tournamentService.GetTournamentById(id);
        }

        [HttpPut("editTournament")]
        public async Task<EditTournamentDTO> EditTournament([FromBody] EditTournamentDTO tournamentDto)
        {
            return await _tournamentService.EditTournament(tournamentDto);
        }
    }
}
