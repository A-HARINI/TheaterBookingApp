using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class UserRegistration
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public long? Contact { get; set; }

    public string? EMail { get; set; }

    public string? PreferedLocation { get; set; }

    public string? NearByTheaterName { get; set; }

    public bool? IsMembershipCardAvailable { get; set; }

    public DateTime? AccountCreatedAt { get; set; }

    public bool? DeletedAt { get; set; }
}
