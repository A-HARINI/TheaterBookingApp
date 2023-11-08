using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class QuerySupportTeam
{
    public long Id { get; set; }

    public int? TeamNo { get; set; }

    public string? TeamName { get; set; }

    public string? PersonName { get; set; }

    public bool? IsQueryResolution { get; set; }

    public string? ResponseStatus { get; set; }
}
