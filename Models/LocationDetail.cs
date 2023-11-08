using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class LocationDetail
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? DistrictName { get; set; }

    public long? Pincode { get; set; }
}
