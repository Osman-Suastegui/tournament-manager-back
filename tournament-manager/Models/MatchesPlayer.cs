using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class MatchesPlayer
{
    public long Clave { get; set; }

    public int? Anotaciones { get; set; }

    public int? Asistencias { get; set; }

    public bool? EnBanca { get; set; }

    public string? Equipo { get; set; }

    public int? Faltas { get; set; }

    public string? Posicion { get; set; }

    public long? Jugador { get; set; }

    public long? ClavePartido { get; set; }

    public long? TeamId { get; set; }

    public virtual Match? ClavePartidoNavigation { get; set; }

    public virtual Usuario? JugadorNavigation { get; set; }

    public virtual Team? Team { get; set; }
}
