using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class DistrictDetail
{
    public long Id { get; set; }

    public string? DistrictName { get; set; }

    public long? StateId { get; set; }
}
