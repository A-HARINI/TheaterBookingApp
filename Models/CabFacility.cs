using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class CabFacility
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? PersonCapacity { get; set; }

    public int? CostOfCab { get; set; }

    public bool? IsAvailable { get; set; }
}
