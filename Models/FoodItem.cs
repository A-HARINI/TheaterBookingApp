using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class FoodItem
{
    public long Id { get; set; }

    public string? TypeFood { get; set; }

    public string? ListsOfFoods { get; set; }

    public bool? IsAvailable { get; set; }

    public DateTime? FoodItemsCreatedAt { get; set; }

    public bool? DeletdAt { get; set; }

    public long? FoodCost { get; set; }
}
