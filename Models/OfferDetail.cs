using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class OfferDetail
{
    public long Id { get; set; }

    public string? OfferType { get; set; }

    public string? Description { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? OfferFromDate { get; set; }

    public DateTime? OfferEndDate { get; set; }

    public DateTime? OfferCreatedAt { get; set; }

    public bool? IsDelete { get; set; }
}
