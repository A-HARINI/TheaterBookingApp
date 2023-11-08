using System;
using System.Collections.Generic;

namespace TheatreBookingApp.Models;

public partial class CabBooking
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? PickupLocation { get; set; }

    public bool? IsApplicableForUpAndDown { get; set; }

    public DateTime? PickupDateAndTime { get; set; }

    public long? EmployeeId { get; set; }

    public long? CabFacilityId { get; set; }

    public bool? IsPaidAmount { get; set; }
}
