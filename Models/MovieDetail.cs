using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class MovieDetail
{
    public long Id { get; set; }

    public string? MovieName { get; set; }

    public string? MovieDescription { get; set; }

    public string? MovieReview { get; set; }

    public string? MovieType { get; set; }

    public long? FdfsId { get; set; }

    public bool? FdfsIsAvailable { get; set; }

    public int? MovieRunnableDays { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? DeletedAt { get; set; }

    public virtual ICollection<OnlineTicketBooking> OnlineTicketBookings { get; set; } = new List<OnlineTicketBooking>();

    public virtual ICollection<TheaterDetail> TheaterDetails { get; set; } = new List<TheaterDetail>();
}
