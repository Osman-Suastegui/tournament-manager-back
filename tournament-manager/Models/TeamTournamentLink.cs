using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class TeamTournamentLink
{
    public long Id { get; set; }

    public bool Active { get; set; }

    public string? Token { get; set; }

    public long? TeamTournamentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TeamsTournament? TeamTournament { get; set; }
}
