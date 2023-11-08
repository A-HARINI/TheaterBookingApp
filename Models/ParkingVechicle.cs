using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class ParkingVechicle
{
    public long Id { get; set; }

    public long? VechicleNumber { get; set; }

    public long? BlockId { get; set; }

    public int? TotalNoVechiclesParked { get; set; }
}
