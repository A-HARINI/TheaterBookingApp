using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class UserBuyOffer
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? OfferId { get; set; }

    public bool? IsGetOffer { get; set; }

    public string? Status { get; set; }
}
