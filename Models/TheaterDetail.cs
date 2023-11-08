using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class TheaterDetail
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? LocationId { get; set; }

    public long? Contact { get; set; }

    public string? Description { get; set; }

    public DateTime? OpeningTime { get; set; }

    public DateTime? ClosingTime { get; set; }

    public string? DoSInsideTheater { get; set; }

    public string? DonotSInsideTheater { get; set; }

    public long? ShowDetailsId { get; set; }

    public DateTime? TheaterCreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public long? MovieDetailsId { get; set; }

    public DateTime? CurrentDate { get; set; }

    public virtual MovieDetail? MovieDetails { get; set; }
}
