using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class UserTournament
{
    public long Id { get; set; }

    public string Role { get; set; } = null!;

    public long TournamentId { get; set; }

    public long UserId { get; set; }

    public virtual Tournament Tournament { get; set; } = null!;

    public virtual Usuario User { get; set; } = null!;
}
