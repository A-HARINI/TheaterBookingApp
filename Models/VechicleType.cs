using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class VechicleType
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? VechicleWheelType { get; set; }
}
