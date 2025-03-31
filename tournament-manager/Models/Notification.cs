using System;
using System.Collections.Generic;

namespace tournament_manager.Models;

public partial class Notification
{
    public int Id { get; set; }

    public long SenderId { get; set; }

    public long ReceiverId { get; set; }

    public string Type { get; set; } = null!;

    public string? Message { get; set; }

    public string? Status { get; set; }

    public string? ActionUrl { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Usuario Receiver { get; set; } = null!;

    public virtual Usuario Sender { get; set; } = null!;
}
