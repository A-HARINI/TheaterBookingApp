using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class VechicleParkBlock
{
    public long Id { get; set; }

    public long? VechicleTypeId { get; set; }

    public string? BlockName { get; set; }

    public int? BlockNo { get; set; }

    public int? StandNo { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? IsCreatedAt { get; set; }
}
