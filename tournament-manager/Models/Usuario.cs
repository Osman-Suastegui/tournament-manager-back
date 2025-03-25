using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<MatchesPlayer> MatchesPlayers { get; set; } = new List<MatchesPlayer>();

    public virtual Player? Player { get; set; }

    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<UserTournament> UserTournaments { get; set; } = new List<UserTournament>();
}
