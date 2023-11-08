using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class FestivalTime
{
    public long Id { get; set; }

    public string? FestivalName { get; set; }

    public DateTime? FestivalStartDate { get; set; }

    public DateTime? FestivalEndDate { get; set; }
}
