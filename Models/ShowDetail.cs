using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class ShowDetail
{
    public long Id { get; set; }

    public string? ShowType { get; set; }

    public long? ShowAvailableTypeId { get; set; }

    public long? RoomId { get; set; }
}
