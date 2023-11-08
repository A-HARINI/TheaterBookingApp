using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class SeatDetail
{
    public long Id { get; set; }

    public int? SeatNo { get; set; }

    public int? RowNo { get; set; }

    public long? SeatCategoryId { get; set; }

    public bool? IsAvailableSeats { get; set; }

    public string? Status { get; set; }
}
