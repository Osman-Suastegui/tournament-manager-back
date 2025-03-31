using Microsoft.EntityFrameworkCore;
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

        public async Task createTournament(CreateTournamentDTO tournamentDto, string createdById)
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

            // SET ADMINS 
            // SEARCH user Id by email and add it to the table user_tournament

            List<string> emailAdmins = tournamentDto.Admins;

            var adminIds = await _context.Usuarios
                .Where(u => emailAdmins.Contains(u.Email))
                .Select(u => u.Id)
                .ToListAsync();
            Console.WriteLine("TEST  SAEDSDSA DASD ASDASD tournament id -> " + tournament.Id);
            foreach(var adminId in adminIds)
            {
                var userTournament = new UserTournament
                {
                    TournamentId = tournament.Id,
                    UserId = adminId,
                    Role = "ORGANIZER"
                    
                };
                await _context.UserTournaments.AddAsync(userTournament);
                await _context.Notifications.AddAsync(new Notification
                {
                    SenderId = long.Parse(createdById), // Convert string to long
                    ReceiverId = adminId,
                    Message = "You have been added as an admin to the tournament " + tournament.Name,
                    Type = "INVITATION"
                });
            }

            
          

            await _context.SaveChangesAsync();
        }
    }
}
