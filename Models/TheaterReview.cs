using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class TheaterReview
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public int? Ratings { get; set; }

    public string? Feedback { get; set; }

    public long? TheaterId { get; set; }
}
