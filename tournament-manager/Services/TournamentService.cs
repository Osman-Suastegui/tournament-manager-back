using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore;
using tournament_manager.DTOS.Tournament;
using tournament_manager.Models;

namespace tournament_manager.Services
{
    public class TournamentService
    {
        private readonly TournamentDbContext _context;

        public TournamentService(TournamentDbContext context)
        {
            _context = context;

        }

        public async Task<TournamentDTO> createTournament(CreateTournamentDTO tournamentDto, string createdById)
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

            var teamEntities = new List<Team>();
            foreach (var teamDto in tournamentDto.Teams)
            {
                var newTeam = new Team
                {
                    Name = teamDto.Name,
                    // set other properties if needed
                };

                await _context.Teams.AddAsync(newTeam);
                teamEntities.Add(newTeam); // Save reference to get ID after SaveChanges
            }
            
            await _context.SaveChangesAsync(); // Save to get auto-generated IDs

            foreach (var team in teamEntities)
            {
                var teamsTournament = new TeamsTournament
                {
                    TeamId = team.Id,
                    TournamentId = tournament.Id
                };
                await _context.TeamsTournaments.AddAsync(teamsTournament);
            }
            await _context.SaveChangesAsync();

            return new TournamentDTO {
                Id = tournament.Id,
                Name = tournament.Name
            };
        }

        public async Task<List<Tournament>> getTournaments(string? q,int limit, int skip,string status)
        {
            IQueryable<Tournament> tournamentsQuery = _context.Tournaments;
            var now = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(q)) 
            {

               tournamentsQuery = tournamentsQuery.Where(tournament => EF.Functions.ILike(tournament.Name, $"%{q}%"));
            }

            if (status == "ongoing")
            {
                tournamentsQuery = tournamentsQuery.Where(t => t.StartDate.HasValue && t.EndDate.HasValue && now >= t.StartDate.Value && now <= t.EndDate.Value);
            }
            else if(status == "completed") { 
                tournamentsQuery = tournamentsQuery.Where(t => t.EndDate.HasValue && now > t.EndDate.Value);
            }else if(status == "upcoming")

            {
                tournamentsQuery = tournamentsQuery.Where(t => t.StartDate.HasValue && now < t.StartDate.Value);
            }

            return await tournamentsQuery
                     .OrderByDescending(t => t.CreatedAt ?? DateTime.MinValue)   // NULL → 0001-01-01
                    .Skip(skip)
                    .Take(limit)
                    .ToListAsync();

        }

        public async Task<Tournament> GetTournamentById(long id) => await _context.Tournaments.FindAsync(id);

        public async Task<EditTournamentDTO> EditTournament(EditTournamentDTO tournamentDto)
        {
            // 1. Load the existing entity
            var entity = await _context.Tournaments
                .FindAsync(tournamentDto.Id);

            if (entity == null)
            {
                // Option A: throw if not found
                throw new KeyNotFoundException($"Tournament with Id {tournamentDto.Id} not found.");

            }

            // 2. Update its properties
            entity.Name = tournamentDto.Name;
            entity.Sport = tournamentDto.Sport;
            //entity.TournamentType = tournamentDto.TournamentType;
            entity.Description = tournamentDto.Description;
            entity.StartDate = tournamentDto.StartDate;
            entity.EndDate = tournamentDto.EndDate;
            //entity.Location = tournamentDto.Location;
            entity.Rules = tournamentDto.Rules;

            // 3. Persist the changes
            await _context.SaveChangesAsync();

            // 4. Return the updated DTO (you could also map back from entity)
            return tournamentDto;
        }

    }
}
