using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class TestEntity
{
    public long Id { get; set; }

    public bool IsDeveloper { get; set; }

    public string? Lastname { get; set; }

    public string Name { get; set; } = null!;
}
