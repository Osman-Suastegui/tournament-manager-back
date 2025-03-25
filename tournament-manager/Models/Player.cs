using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Player
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? UserId { get; set; }

    public virtual ICollection<PlayersTournament> PlayersTournaments { get; set; } = new List<PlayersTournament>();

    public virtual Usuario? User { get; set; }
}
