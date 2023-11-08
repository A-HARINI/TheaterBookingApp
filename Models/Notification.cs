using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class Notification
{
    public long Id { get; set; }

    public long? BookingId { get; set; }

    public string? NotificationType { get; set; }

    public string? Status { get; set; }

    public DateTime? IsCreatedAt { get; set; }

    public bool? IsDelete { get; set; }
}
