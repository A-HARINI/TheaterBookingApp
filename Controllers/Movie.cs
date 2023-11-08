using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("MovieBooking")]

    public class MovieBookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieBookingController(AppDbContext context)
        {
            _context = context;
        }


    
    [HttpPost("moviedetails")]
    public IActionResult CreateMovieDetails([FromBody] MovieDetailsDto movieDto)
    {
        if (movieDto == null)
        {
            return BadRequest("Invalid movie data.");
        }

       
        var movieDetail = new MovieDetail
        {
            MovieName = movieDto.MovieName,
            MovieDescription = movieDto.MovieDescription,
            MovieReview = movieDto.MovieReview,
            MovieType = movieDto.MovieType,
            FdfsId = movieDto.FdfsId,
            FdfsIsAvailable = movieDto.FdfsIsAvailable,
            MovieRunnableDays = movieDto.MovieRunnableDays,
            IsAvailable = movieDto.IsAvailable,
            CreatedAt = DateTime.Now, 
            DeletedAt = false 
        };

        
        _context.MovieDetails.Add(movieDetail);
        _context.SaveChanges();

        return Ok(new{
            data = movieDetail,
            StatusCode = 200,
            message ="Movie detail created successfully."});
    }

   
    [HttpGet("{id}")]
    public IActionResult GetMovieDetails(long id)
    {
        
        var movieDetail = _context.MovieDetails.FirstOrDefault(m => m.Id == id);
        if (movieDetail != null)
        {
            var movieDto = new MovieDetailsDto
            {
                MovieName = movieDetail.MovieName,
                MovieDescription = movieDetail.MovieDescription,
                MovieReview = movieDetail.MovieReview,
                MovieType = movieDetail.MovieType,
                FdfsId = movieDetail.FdfsId,
                FdfsIsAvailable = movieDetail.FdfsIsAvailable,
                MovieRunnableDays = movieDetail.MovieRunnableDays,
                IsAvailable = movieDetail.IsAvailable
            };
            return Ok(new{
                data = movieDto,
                StatusCode = 200,
                message =" movie details are listed"
        });

    
    } return NotFound("Movie detail not found.");}
[HttpPost("firstdayfirstshowdetails")]
public IActionResult CreateFirstdayFirstshowDetail([FromBody] FirstdayFirstshowDetailDTO dto)
{
    if (dto == null)
    {
        return BadRequest("Invalid data.");
    }

   
    if (string.IsNullOrEmpty(dto.MovieName))
    {
        ModelState.AddModelError("MovieName", "MovieName is required.");
        return BadRequest(ModelState);
    }

    if (dto.MovieReleasingDate == null)
    {
        ModelState.AddModelError("MovieReleasingDate", "MovieReleasingDate is required.");
        return BadRequest(ModelState);
    }

    var entity = new FirstdayFirstshowDetail
    {
        MovieName = dto.MovieName,
        
       
        MovieReleasingDate = DateTime.SpecifyKind(dto.MovieReleasingDate.Value, DateTimeKind.Unspecified),
        OnlineReservationOpeningTime = dto.OnlineReservationOpeningTime.HasValue ? DateTime.SpecifyKind(dto.OnlineReservationOpeningTime.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        OnlineResrvationClosingTime = dto.OnlineResrvationClosingTime.HasValue ? DateTime.SpecifyKind(dto.OnlineResrvationClosingTime.Value, DateTimeKind.Unspecified) : (DateTime?)null
    };

    if (dto.OnlineReservationOpeningTime >= dto.MovieReleasingDate)
    {
        ModelState.AddModelError("OnlineReservationOpeningTime", "OnlineReservationOpeningTime must be before MovieReleasingDate.");
        return BadRequest(ModelState);
    }

    if (dto.OnlineResrvationClosingTime >= dto.MovieReleasingDate)
    {
        ModelState.AddModelError("OnlineResrvationClosingTime", "OnlineResrvationClosingTime must be before MovieReleasingDate.");
        return BadRequest(ModelState);
    }

    _context.FirstdayFirstshowDetails.Add(entity);
    _context.SaveChanges();

    return Ok(new
    {
        data = entity,
        StatusCode = 200,
        message = "This movies FirstdayFirstshowDetail created successfully."
    });
}

[HttpGet("firstdayfirstshowdetails")]
public IActionResult GetFirstdayFirstshowDetails()
{
    var entities = _context.FirstdayFirstshowDetails.ToList(); 
    var count = entities.Count; 

    
    var response = new
    {
        data = entities,
        StatusCode = 200,
        message = "FirstdayFirstshowDetails retrieved successfully.",
        count = count
    };

    return Ok(response);
}



  

[HttpPost("showdetails")]
public async Task<IActionResult> CreateShow([FromBody] ShowDetailsDTO showDTO)
{
    if (showDTO == null)
    {
        return BadRequest("Invalid show data.");
    }

   
    if (string.IsNullOrWhiteSpace(showDTO.ShowType))
    {
        ModelState.AddModelError("ShowType", "ShowType is required.");
        return BadRequest(ModelState);
    }

    if (!_context.ShowAvailableTypes.Any(sat => sat.Id == showDTO.ShowAvailableTypeID))
    {
        ModelState.AddModelError("ShowAvailableTypeID", "Invalid Show Available Type ID.");
        return BadRequest(ModelState);
    }

    if (!_context.RoomDetails.Any(room => room.Id == showDTO.RoomID))
    {
        ModelState.AddModelError("RoomID", "Invalid Room ID.");
        return BadRequest(ModelState);
    }

    var show = new ShowDetail
    {
        ShowType = showDTO.ShowType,
        ShowAvailableTypeId = showDTO.ShowAvailableTypeID,
        RoomId = showDTO.RoomID
       
    };

    _context.ShowDetails.Add(show);
    var result = await _context.SaveChangesAsync();

if (result > 0)
{
    var response = new
    {
        data = showDTO,
        StatusCode = 201, 
        message = "Show created successfully."
    };

    return Created($"api/shows/{show.Id}", response);
}
    else
    {
       
        return StatusCode(500, new
        {
            StatusCode = 500,
            message = "An error occurred while creating ShowDetail."
            
        });
    }
}




[HttpGet("showdetails")]

public IActionResult GetShowDetails()
{
    var showDetails = _context.ShowDetails.ToList();

    return Ok(new
    {
        data = showDetails,
        StatusCode = 200,
        message = "ShowDetails retrieved successfully."
    });
} 
   [HttpPost("showavailability")]
    public IActionResult CreateShowAvailableType([FromBody] ShowAvailableTypesDTO showAvailableTypesDTO)
    {
        if (showAvailableTypesDTO == null)
        {
            return BadRequest("Invalid show available type data.");
        }

      
        if (showAvailableTypesDTO.MovieID <= 0 || string.IsNullOrEmpty(showAvailableTypesDTO.AvailableShowTimes))
        {
            return BadRequest("Invalid data. Please provide valid values for MovieID and AvailableShowTimes.");
        }

        var showAvailableType = new ShowAvailableType
        {
            MovieId = showAvailableTypesDTO.MovieID,
            AvailableShowTimes = showAvailableTypesDTO.AvailableShowTimes
           
        };

        try
        {
            _context.ShowAvailableTypes.Add(showAvailableType);
            _context.SaveChanges();

            return Ok(new
            {
                data = showAvailableType,
                StatusCode = 200,
                message = "ShowAvailableType created successfully."
            });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating ShowAvailableType.",
                error = ex.Message 
            });
        }
    }

    
    [HttpGet("Getshowavailability")]
    public IActionResult GetShowAvailableTypes()
    {
        var showAvailableTypes = _context.ShowAvailableTypes.ToList();

        return Ok(new
        {
            data = showAvailableTypes,
            count = showAvailableTypes.Count,
            StatusCode = 200,
            message = "ShowAvailableTypes retrieved successfully."
        });
    }


    [HttpPost("Roomdetails")]
    public IActionResult CreateRoomDetail([FromBody] RoomDetailsDTO roomDetailsDTO)
    {
        if (roomDetailsDTO == null)
        {
            return BadRequest("Invalid room detail data.");
        }

        
        if (roomDetailsDTO.RoomNo <= 0 || roomDetailsDTO.BlockID <= 0)
        {
            return BadRequest("Invalid data. Please provide valid values for RoomNo and BlockID.");
        }

        var roomDetail = new RoomDetail
        {
            RoomNo = roomDetailsDTO.RoomNo,
            BlockId = roomDetailsDTO.BlockID
            
        };

        try
        {
            _context.RoomDetails.Add(roomDetail);
            _context.SaveChanges();

            return Ok(new
            {
                data = roomDetail,
                StatusCode = 200,
                message = "RoomDetail created successfully."
            });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating RoomDetail.",
                error = ex.Message 
            });
        }
    }

   
    [HttpGet("Get Roomdetails")]
    public IActionResult GetRoomDetails()
    {
        var roomDetails = _context.RoomDetails.ToList();

        return Ok(new
        {
            data = roomDetails,
            count = roomDetails.Count, 
            StatusCode = 200,
            message = "RoomDetails retrieved successfully."
        });
    }

    [HttpPost("Blockdetails")]
    public IActionResult CreateBlocksDetail([FromBody] BlocksDetailsDTO blocksDetailsDTO)
    {
        if (blocksDetailsDTO == null)
        {
            return BadRequest("Invalid blocks detail data.");
        }

       
        if (string.IsNullOrEmpty(blocksDetailsDTO.BlockCategory))
        {
            return BadRequest("BlockCategory is required.");
        }

        var blocksDetail = new BlocksDetail
        {
            BlockCategory = blocksDetailsDTO.BlockCategory,
            SeatId = blocksDetailsDTO.SeatID,
            ClassId = blocksDetailsDTO.ClassID,
            IsAvailable = blocksDetailsDTO.IsAvailable
          
        };

        try
        {
            _context.BlocksDetails.Add(blocksDetail);
            _context.SaveChanges();

            return Ok(new
            {
                data = blocksDetail,
                StatusCode = 200,
                message = "BlocksDetail created successfully."
            });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating BlocksDetail.",
                error = ex.Message 
            });
        }
    }

 
    [HttpGet("getblockdetails")]
    public IActionResult GetBlocksDetails()
    {
        var blocksDetails = _context.BlocksDetails.ToList();

        return Ok(new
        {
            data = blocksDetails,
            count = blocksDetails.Count, 
            StatusCode = 200,
            message = "BlocksDetails retrieved successfully."
        });
    }

    [HttpPost("seatdetails")]
    public IActionResult CreateSeatDetail([FromBody] SeatDetailsDTO seatDetailsDTO)
    {
        if (seatDetailsDTO == null)
        {
            return BadRequest("Invalid seat detail data.");
        }

        
        if (seatDetailsDTO.SeatNO <= 0)
        {
            return BadRequest("SeatNO must be a positive number.");
        }

        var seatDetail = new SeatDetail
        {
            SeatNo = seatDetailsDTO.SeatNO,
            RowNo = seatDetailsDTO.RowNO,
            SeatCategoryId = seatDetailsDTO.SeatCategoryID,
            IsAvailableSeats = seatDetailsDTO.IsAvailableSeats,
            Status = seatDetailsDTO.Status
           
        };

        try
        {
            _context.SeatDetails.Add(seatDetail);
            _context.SaveChanges();

            return Ok(new
            {
                data = seatDetail,
                StatusCode = 200,
                message = "SeatDetail created successfully."
            });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating SeatDetail.",
                error = ex.Message 
            });
        }
    }

    // GET api/seatdetails
    [HttpGet("getseatdetails")]
    public IActionResult GetSeatDetails()
    {
        var seatDetails = _context.SeatDetails.ToList();

        return Ok(new
        {
            data = seatDetails,
            count = seatDetails.Count, 
            StatusCode = 200,
            message = "SeatDetails retrieved successfully."
        });
    }
    [HttpPost("classcategories")]
    public IActionResult CreateClassesCategory([FromBody] ClassesCategoriesDTO classesCategoriesDTO)
    {
        if (classesCategoriesDTO == null)
        {
            return BadRequest("Invalid class category data.");
        }

        if (string.IsNullOrWhiteSpace(classesCategoriesDTO.ClassType))
        {
            return BadRequest("ClassType is required.");
        }

        var classesCategory = new ClassesCategory
        {
            ClassType = classesCategoriesDTO.ClassType
           
        };

        try
        {
            _context.ClassesCategories.Add(classesCategory);
            _context.SaveChanges();

            return Ok(new
            {
                data = classesCategory,
                StatusCode = 200,
                message = "ClassesCategory created successfully."
            });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating ClassesCategory.",
                error = ex.Message 
            });
        }
    }

   
    [HttpGet("getclasscategories")]
    public IActionResult GetClassesCategories()
    {
        var classesCategories = _context.ClassesCategories.ToList();

        return Ok(new
        {
            data = classesCategories,
            count = classesCategories.Count, 
            StatusCode = 200,
            message = "ClassesCategories retrieved successfully."
        });
    }

 [HttpPost("seatcategory")]
    public IActionResult CreateSeatCategory([FromBody] SeatCategoryDTO seatCategoryDTO)
    {
        if (seatCategoryDTO == null)
        {
            return BadRequest("Invalid seat category data.");
        }

        
        if (string.IsNullOrWhiteSpace(seatCategoryDTO.SeatType))
        {
            return BadRequest("SeatType is required.");
        }

        var seatCategory = new SeatCategory
        {
            SeatType = seatCategoryDTO.SeatType
            
        };

        try
        {
            _context.SeatCategories.Add(seatCategory);
            _context.SaveChanges();

            return Ok(new
            {
                data = seatCategory,
                StatusCode = 200,
                message = "SeatCategory created successfully."
            });
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, new
            {
                StatusCode = 500,
                message = "An error occurred while creating SeatCategory.",
                error = ex.Message // Include error details if needed
            });
        }
    }


    [HttpGet("getseatcategory")]
    public IActionResult GetSeatCategories()
    {
        var seatCategories = _context.SeatCategories.ToList();

        return Ok(new
        {
            data = seatCategories,
            count = seatCategories.Count, 
            StatusCode = 200,
            message = "SeatCategories retrieved successfully."
        });
    } 
}










   
    