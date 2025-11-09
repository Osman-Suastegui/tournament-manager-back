namespace tournament_manager.DTOS.Tournament
{
    public class TournamentDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; } = string.Empty; // Added missing semicolon  
    }
}
