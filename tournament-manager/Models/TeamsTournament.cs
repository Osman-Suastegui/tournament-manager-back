using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class TeamsTournament
{
    public long Id { get; set; }

    public long? TeamId { get; set; }

    public long? TournamentId { get; set; }

    public virtual ICollection<PlayersTournament> PlayersTournaments { get; set; } = new List<PlayersTournament>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<TeamTournamentLink> TeamTournamentLinks { get; set; } = new List<TeamTournamentLink>();

    public virtual Tournament? Tournament { get; set; }
}
