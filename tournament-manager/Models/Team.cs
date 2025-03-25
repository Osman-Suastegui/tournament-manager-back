using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Team
{
    public long Id { get; set; }

    public string? Logo { get; set; }

    public string Name { get; set; } = null!;

    public long? AdminEquipo { get; set; }

    public virtual Usuario? AdminEquipoNavigation { get; set; }

    public virtual ICollection<Match> MatchTeam1s { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeam2s { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchWinners { get; set; } = new List<Match>();

    public virtual ICollection<MatchesPlayer> MatchesPlayers { get; set; } = new List<MatchesPlayer>();

    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();

    public virtual ICollection<TeamsTournament> TeamsTournaments { get; set; } = new List<TeamsTournament>();
}
