using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class EmployeeDetail
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public long? Contact { get; set; }
}
