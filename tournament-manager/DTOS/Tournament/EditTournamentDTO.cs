namespace tournament_manager.DTOS.Tournament
{
    public class EditTournamentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sport { get; set; }
        public TournamentType TournamentType { get; set; }  // Changed to use the enum
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Rules { get; set; }
        public DateTime StartDate { get; set; }
    }
}
