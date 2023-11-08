using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class FirstdayFirstshowDetail
{
    public long Id { get; set; }

    public string? MovieName { get; set; }

    public DateTime? MovieReleasingDate { get; set; }

    public DateTime? OnlineReservationOpeningTime { get; set; }

    public DateTime? OnlineResrvationClosingTime { get; set; }
}
