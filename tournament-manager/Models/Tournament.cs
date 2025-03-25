using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Tournament
{
    public long Id { get; set; }

    public int? CantidadEquipos { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Estado { get; set; }

    public string? Name { get; set; }

    public string? Sport { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? CantidadEnfrentamientosRegular { get; set; }

    public int? CantidadPlayoffs { get; set; }

    public short? TournamentType { get; set; }

    public string? Rules { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<TeamsTournament> TeamsTournaments { get; set; } = new List<TeamsTournament>();

    public virtual ICollection<UserTournament> UserTournaments { get; set; } = new List<UserTournament>();
}
