using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class FoodCounter
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? DirectionFromTheMovieRoom { get; set; }
}
