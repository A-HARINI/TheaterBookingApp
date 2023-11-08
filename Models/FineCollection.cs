using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class FineCollection
{
    public long Id { get; set; }

    public long? BookingId { get; set; }

    public long? TotalCost { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsBookedCase { get; set; }
}
