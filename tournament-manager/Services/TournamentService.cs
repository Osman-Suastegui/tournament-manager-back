using tournament_manager.DTOS.Tournament;
using tournament_manager.Models;

namespace tournament_manager.Services
{
    public class TournamentService
    {
        private readonly ApplicationDbContext _context;

        public TournamentService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task createTournament(CreateTournamentDTO tournamentDto)
        {
            var tournament = new Tournament
            {
                Name = tournamentDto.Name,
                Sport = tournamentDto.Sport,
                TournamentType = (short) tournamentDto.TournamentType,
                Description = tournamentDto.Description,
                //Location = tournamentDto.Location,
                Rules = tournamentDto.Rules,
                // Set other properties as needed
            };

            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
        }
    }
}
