using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class HelpDeskQuery
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? Queries { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDelete { get; set; }

    public long? ResponseBy { get; set; }
}
