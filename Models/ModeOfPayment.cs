using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class ModeOfPayment
{
    public long Id { get; set; }

    public string? TypeOfPaymentMode { get; set; }
}
