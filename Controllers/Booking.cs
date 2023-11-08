using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("Booked")]

    public class bookedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public bookedController(AppDbContext context)
        {
            _context = context;
        }
  [HttpPost("Image")]
    public async Task<ResponseDTO> AddImage(IFormFile file, int id)
    {


        var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = "./wwwroot/Images/" + fileName;

        var sourceStream = file.OpenReadStream();
        var destinationStream = new FileStream(filePath, FileMode.Create);

        await sourceStream.CopyToAsync(destinationStream);

        var user = await _context.OnlineTicketScancopies.AddAsync(new OnlineTicketScancopy{
            Id = id,
            ScannedCopy= fileName
        

        });

        
        await _context.SaveChangesAsync();

        return new ResponseDTO
        {
            Data = "Images/" + user,
            Message = "Your ticket copy",
            Status = TheatreBookingApp.Response.sucess

        };
        
    }

    [HttpGet("Image")]
    public IActionResult GetImage(){
        var result =_context.OnlineTicketScancopies.ToList();
        return Ok (result);
    }
   [HttpPost("ticketbookings")]
public IActionResult CreateTicketBooking([FromBody] OnlineTicketBookingDTO onlineTicketBookingDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    if (onlineTicketBookingDTO.TicketBookingDate == null)
    {
        ModelState.AddModelError("TicketBookingDate", "Ticket booking date is required.");
        return BadRequest(ModelState);
    }

    
    var onlineTicketBooking = new OnlineTicketBooking
    {
        UserId = onlineTicketBookingDTO.UserId,
        TicketBookingDate = onlineTicketBookingDTO.TicketBookingDate.HasValue ? DateTime.SpecifyKind(onlineTicketBookingDTO.TicketBookingDate.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        TheaterDetails = onlineTicketBookingDTO.TheaterDetails,
        OnlineTicketCounter = onlineTicketBookingDTO.OnlineTicketCounter,
            OrderItemId = onlineTicketBookingDTO.OrderItemId,
            IsParkingVechicle = onlineTicketBookingDTO.IsParkingVechicle,
            IsCabBooking = onlineTicketBookingDTO.IsCabBooking,
            MovieDetailsId =onlineTicketBookingDTO.MovieDetailsId,
            CreatedAt = DateTime.Now,
            Status = onlineTicketBookingDTO.Status,
            ScannedCopy = onlineTicketBookingDTO.ScannedCopy
        };
       
   

   
    _context.OnlineTicketBookings.Add(onlineTicketBooking);
    _context.SaveChanges();

    return Ok(new
    {
        data = onlineTicketBooking,
        StatusCode = 200,
        message = "Online ticket booking created successfully."
    });
}

[HttpGet("ticketbookings")]
public IActionResult GetTicketBookings()
{
    
    var ticketBookings = _context.OnlineTicketBookings.ToList();
    int bookedCount = _context.OnlineTicketBookings.Count(o => o.Status == "booked confirmation");
    int overallCost = 5000;

    if (ticketBookings == null || ticketBookings.Count == 0)
    {
        return NotFound("No ticket bookings found.");
    }

   
    return Ok(new
    {
        data = ticketBookings,ticketBookings.Count,bookedCount,
            overallCost,
        StatusCode = 200,
        message = "Online ticket bookings retrieved successfully."
    });
}


  
    // [HttpGet("GetBookedconfirmedTickets")]
    // public IActionResult GetOnlineTicketBookings()
    // {
    //     var onlineTicketBookings = _context.OnlineTicketBookings.Include(o => o.UserId); 

    
    //     int bookedCount = onlineTicketBookings.Count(o => o.Status == "Booked");

 
    //     int totalCount = onlineTicketBookings.Count();

   
  
    //     decimal overallCost = 0;

    //     return Ok(new
    //     {
    //         data = onlineTicketBookings,
    //         StatusCode = 200,
    //         message = "Online ticket bookings retrieved successfully.",
    //         bookedCount,
    //         totalCount,
    //         overallCost
    //     });
    // }

    [HttpPost("parkingvehicles")]
public IActionResult CreateParkingVehicle([FromBody] ParkingVechicleDTO parkingVehicleDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    if (parkingVehicleDTO.VechicleNumber == null)
    {
        ModelState.AddModelError("VechicleNumber", "Vehicle number is required.");
        return BadRequest(ModelState);
    }

    if (parkingVehicleDTO.BlockId == null)
    {
        ModelState.AddModelError("BlockId", "Block ID is required.");
        return BadRequest(ModelState);
    }

    
    var parkingVehicle = new ParkingVechicle
    {
        VechicleNumber = parkingVehicleDTO.VechicleNumber,
        BlockId = parkingVehicleDTO.BlockId,
        TotalNoVechiclesParked = parkingVehicleDTO.TotalNoVechiclesParked
       
    };

    
    _context.ParkingVechicles.Add(parkingVehicle);
    _context.SaveChanges();

    return Ok(new
    {
        data = parkingVehicle,
        StatusCode = 200,
        message = "Parking vehicle created successfully."
    });
}
[HttpGet("parkingvehicles")]
public IActionResult GetParkingVehicles()
{
    var parkingVehicles = _context.ParkingVechicles.ToList();

    
    var totalCount = parkingVehicles.Count;

    return Ok(new
    {
        data = parkingVehicles,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Parking vehicles retrieved successfully."
    });
}
[HttpPost("vehicletypes")]
public IActionResult CreateVehicleType([FromBody] VechicleTypeDTO vehicleTypeDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    var vehicleType = new VechicleType
    {
        Name = vehicleTypeDTO.Name,
        VechicleWheelType = vehicleTypeDTO.VechicleWheelType
       
    };

   
    _context.VechicleTypes.Add(vehicleType);
    _context.SaveChanges();

    return Ok(new
    {
        data = vehicleType,
        StatusCode = 200,
        message = "Vehicle type created successfully."
    });
}
[HttpGet("vehicletypes")]
public IActionResult GetVehicleTypes()
{
    var vehicleTypes = _context.VechicleTypes.ToList();

    
    var totalCount = vehicleTypes.Count;

    return Ok(new
    {
        data = vehicleTypes,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Vehicle types retrieved successfully."
    });
}
[HttpPost("vehicleparkblocks")]
public IActionResult CreateVehicleParkBlock([FromBody] VechicleParkBlockDTO vehicleParkBlockDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    var vehicleParkBlock = new VechicleParkBlock
    {
        VechicleTypeId = vehicleParkBlockDTO.VechicleTypeId,
        BlockName = vehicleParkBlockDTO.BlockName,
        BlockNo = vehicleParkBlockDTO.BlockNo,
        StandNo = vehicleParkBlockDTO.StandNo,
        IsAvailable = vehicleParkBlockDTO.IsAvailable,
        
    };

   
    _context.VechicleParkBlocks.Add(vehicleParkBlock);
    _context.SaveChanges();

    return Ok(new
    {
        data = vehicleParkBlock,
        StatusCode = 200,
        message = "Vehicle park block created successfully."
    });
}
[HttpGet("vehicleparkblocks")]
public IActionResult GetVehicleParkBlocks()
{
    var vehicleParkBlocks = _context.VechicleParkBlocks.ToList();

  
    var totalCount = vehicleParkBlocks.Count;

    return Ok(new
    {
        data = vehicleParkBlocks,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Vehicle park blocks retrieved successfully."
    });
}
[HttpPost("cabfacilities")]
public IActionResult CreateCabFacility([FromBody] CabFacilityDTO cabFacilityDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    var cabFacility = new CabFacility
    {
        Name = cabFacilityDTO.Name,
        Type = cabFacilityDTO.Type,
        PersonCapacity = cabFacilityDTO.PersonCapacity,
        CostOfCab = cabFacilityDTO.CostOfCab,
        IsAvailable = cabFacilityDTO.IsAvailable
        
    };

    
    _context.CabFacilities.Add(cabFacility);
    _context.SaveChanges();

    return Ok(new
    {
        data = cabFacility,
        StatusCode = 200,
        message = "Cab facility created successfully."
    });
}

[HttpGet("cabfacilities")]
public IActionResult GetCabFacilities()
{
    var cabFacilities = _context.CabFacilities.ToList();

    
    var totalCount = cabFacilities.Count;

    return Ok(new
    {
        data = cabFacilities,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Cab facilities retrieved successfully."
    });
}
[HttpPost("cabbookings")]
public IActionResult CreateCabBooking([FromBody] CabBookingDTO cabBookingDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

  
    var cabBooking = new CabBooking
    {
        UserId = cabBookingDTO.UserId,
        PickupLocation = cabBookingDTO.PickupLocation,
        IsApplicableForUpAndDown = cabBookingDTO.IsApplicableForUpAndDown,
        PickupDateAndTime = cabBookingDTO.PickupDateAndTime.HasValue ? DateTime.SpecifyKind(cabBookingDTO.PickupDateAndTime.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        EmployeeId = cabBookingDTO.EmployeeId,
        CabFacilityId = cabBookingDTO.CabFacilityId,
        IsPaidAmount = cabBookingDTO.IsPaidAmount
        
    };

  
    _context.CabBookings.Add(cabBooking);
    _context.SaveChanges();

    return Ok(new
    {
        data = cabBooking,
        StatusCode = 200,
        message = "Cab booking created successfully."
    });
}
[HttpGet("cabbookings")]
public IActionResult GetCabBookings()
{
    var cabBookings = _context.CabBookings.ToList();

    
    var totalCount = cabBookings.Count;

    return Ok(new
    {
        data = cabBookings,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Cab bookings retrieved successfully."
    });
}
[HttpPost("employeedetails")]
public IActionResult CreateEmployeeDetail([FromBody] EmployeeDetailDTO employeeDetailDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var employeeDetail = new EmployeeDetail
    {
        Name = employeeDetailDTO.Name,
        Role = employeeDetailDTO.Role,
        Contact = employeeDetailDTO.Contact
       
    };

   
    _context.EmployeeDetails.Add(employeeDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = employeeDetail,
        StatusCode = 200,
        message = "Employee detail created successfully."
    });
}
[HttpGet("employeedetails")]
public IActionResult GetEmployeeDetails()
{
    var employeeDetails = _context.EmployeeDetails.ToList();

  
    var totalCount = employeeDetails.Count;

    return Ok(new
    {
        data = employeeDetails,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Employee details retrieved successfully."
    });
}

 [HttpPost("notifications")]
public async Task<IActionResult> CreateNotificationsForBookedConfirmed()
{
   
    var bookings = await _context.OnlineTicketBookings
        .Where(b => b.Status == "booked confirmation")
        .ToListAsync();

    if (bookings.Count == 0)
    {
        return BadRequest("No bookings with status 'BookedConfirmed' found.");
    }

    var notifications = new List<Notification>();

    foreach (var booking in bookings)
    {
        
        var notification = new Notification
        {
            BookingId = booking.Id,
            NotificationType = "Message", 
            Status = "Enj the movie ",          
            IsCreatedAt = DateTime.Now,
         
        };

        notifications.Add(notification);
    }

    _context.Notifications.AddRange(notifications);
    await _context.SaveChangesAsync();

    return Ok(new
    {
        data = notifications,
        count = notifications.Count,
        StatusCode = 200,
        message = "Notifications created successfully for 'BookedConfirmed' bookings."
    });
}

    }
