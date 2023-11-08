using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class ShowAvailableType
{
    public long Id { get; set; }

    public long? MovieId { get; set; }

    public string? AvailableShowTimes { get; set; }
}
