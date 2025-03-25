using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class PlayersTournament
{
    public long Id { get; set; }

    public long? PlayerId { get; set; }

    public long? TeamTournamentId { get; set; }

    public virtual Player? Player { get; set; }

    public virtual TeamsTournament? TeamTournament { get; set; }
}
