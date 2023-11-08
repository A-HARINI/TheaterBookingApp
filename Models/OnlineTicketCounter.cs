using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class OnlineTicketCounter
{
    public long Id { get; set; }

    public string? CounterName { get; set; }

    public int? CounterNumber { get; set; }

    public long? EmployeeId { get; set; }
}
