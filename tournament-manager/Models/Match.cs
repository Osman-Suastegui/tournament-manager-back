using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Match
{
    public long Id { get; set; }

    public int? Round { get; set; }

    public DateTime? FechaInicio { get; set; }

    public long? RefereeId { get; set; }

    public long? NextMatchId { get; set; }

    public long? Team1Id { get; set; }

    public long? Team2Id { get; set; }

    public long? TournamentId { get; set; }

    public long? WinnerId { get; set; }

    public bool IsDraw { get; set; }

    public virtual ICollection<Match> InverseNextMatch { get; set; } = new List<Match>();

    public virtual ICollection<MatchesPlayer> MatchesPlayers { get; set; } = new List<MatchesPlayer>();

    public virtual Match? NextMatch { get; set; }

    public virtual Usuario? Referee { get; set; }

    public virtual Team? Team1 { get; set; }

    public virtual Team? Team2 { get; set; }

    public virtual Tournament? Tournament { get; set; }

    public virtual Team? Winner { get; set; }
}
