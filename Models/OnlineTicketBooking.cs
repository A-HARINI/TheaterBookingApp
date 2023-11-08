using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class OnlineTicketBooking
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public DateTime? TicketBookingDate { get; set; }

    public long? TheaterDetails { get; set; }

    public long? OnlineTicketCounter { get; set; }

    public long? OrderItemId { get; set; }

    public long? IsParkingVechicle { get; set; }

    public long? IsCabBooking { get; set; }

    public long? TotalAmountCollected { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? ScannedCopy { get; set; }

    public long? MovieDetailsId { get; set; }

    public virtual MovieDetail? MovieDetails { get; set; }
}
