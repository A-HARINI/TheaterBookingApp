﻿using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class OnlineTicketScancopy
{
    public long Id { get; set; }

    public string? ScannedCopy { get; set; }
}
