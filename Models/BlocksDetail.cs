using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class BlocksDetail
{
    public long Id { get; set; }

    public string? BlockCategory { get; set; }

    public long? SeatId { get; set; }

    public long? ClassId { get; set; }

    public bool? IsAvailable { get; set; }
}
