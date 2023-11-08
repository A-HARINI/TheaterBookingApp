using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class StateDetail
{
    public long Id { get; set; }

    public string? StateName { get; set; }

    public long? CountryId { get; set; }
}
