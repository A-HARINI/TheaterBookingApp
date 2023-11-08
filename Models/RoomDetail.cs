using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class RoomDetail
{
    public long Id { get; set; }

    public int? RoomNo { get; set; }

    public long? BlockId { get; set; }
}
