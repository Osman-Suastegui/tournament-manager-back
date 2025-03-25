using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class TeamPlayer
{
    public long Id { get; set; }

    public string? Position { get; set; }

    public long? PlayerId { get; set; }

    public long? TeamId { get; set; }

    public virtual Usuario? Player { get; set; }

    public virtual Team? Team { get; set; }
}
