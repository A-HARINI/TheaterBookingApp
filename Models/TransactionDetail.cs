using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class TransactionDetail
{
    public long Id { get; set; }

    public long? BookingId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public long? ModeOfPayment { get; set; }

    public int? TotalAmountReceived { get; set; }

    public long? TransactionId { get; set; }
}
