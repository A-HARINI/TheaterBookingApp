using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? FoodItemsId { get; set; }

    public long? QtyItem { get; set; }

    public long? TotalCost { get; set; }

    public long? FoodCounterId { get; set; }

    public DateTime? OrederDate { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsCancelled { get; set; }
}
