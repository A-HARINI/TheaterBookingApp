using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class PaymentDetail
{
    public long Id { get; set; }

    public long? TransactionDetailsId { get; set; }

    public string? Status { get; set; }

    public string? InvoiceDetails { get; set; }
}
