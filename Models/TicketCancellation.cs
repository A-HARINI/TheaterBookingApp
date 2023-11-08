using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class TicketCancellation
{
    public long Id { get; set; }

    public long? TicketBookingId { get; set; }

    public DateTime? DateOfCancellation { get; set; }

    public bool? IsRefundable { get; set; }
}
