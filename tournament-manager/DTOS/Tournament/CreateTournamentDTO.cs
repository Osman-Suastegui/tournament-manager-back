using System.ComponentModel.DataAnnotations;

namespace tournament_manager.DTOS.Tournament
{
    public enum TournamentType
    {
        SingleElimination,
        DoubleElimination
    }
    public class CreateTournamentDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; }
        public TournamentType TournamentType { get; set; }  // Changed to use the enum
        public List<string> Admins { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Rules { get; set; }
        public DateTime StartDate { get; set; }
        public List<TeamDTO> Teams { get; set; }
    }

    public class TeamDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Team name is required.")]
        public string Name { get; set; } = string.Empty;
        public string LeaderEmail { get; set; }
    }

}
