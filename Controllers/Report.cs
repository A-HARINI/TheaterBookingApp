using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("REPORTS")]

    public class REPORTSController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IServices _services;

        public REPORTSController(AppDbContext context,IServices services)
        {
            _context = context;
            _services =services;
        }
        [HttpGet("most-ordered-food-items-yesterday")]
    public IActionResult GetMostOrderedFoodItemsForYesterday()
    {
        try
        {
            var response = _services.GetMostOrderedFoodItemsForYesterday();
            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ResponseDTO
            {
                Data = null,
                Status = TheatreBookingApp.Response.error,
                Message = $"An error occurred: {ex.Message}"
            };
            return BadRequest(errorResponse);
        }
    }

//  [HttpGet("highestsellingmovies/{year}")]
//     public IActionResult GetHighestSellingMovies(int year)
//     {
//         var highestSellingMovies = _services.GetHighestSellingMoviesByYear(year);

//         return Ok(Response);
    
//     }

 [HttpGet("highest-selling/{year}")]
        public async Task<IActionResult> GetHighestSellingMoviesByYear(int year)
        {
            try
            {
                var highestSellingMovies = await _context.OnlineTicketBookings
                    .Where(booking => booking.TicketBookingDate.HasValue && booking.TicketBookingDate.Value.Year == year)
                    .GroupBy(booking => booking.MovieDetailsId)
                    .Select(group => new
                    {
                        MovieName = group.FirstOrDefault().MovieDetails.MovieName,
                        TotalAmountCollected = group.Sum(booking => booking.TotalAmountCollected ?? 0)
                    })
                    .OrderByDescending(movie => movie.TotalAmountCollected)
                    .ToListAsync();

                return Ok(new
                {
                    Data = highestSellingMovies,
                    Status = "success",
                    Message = "Highest-selling movies retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "An error occurred while fetching the highest-selling movies.",
                    Details = ex.Message
                });
            }
        }
    


        
[HttpGet("theaters/{movieName}")]
public IActionResult GetTheatersForMovie(string movieName)
{
    
    DateTime currentDate = DateTime.Now.Date;

    var theatersForMovieToday = _context.TheaterDetails
        .Where(theater =>
            theater.MovieDetails != null &&
            theater.MovieDetails.MovieName == movieName &&
            theater.CurrentDate.HasValue &&
            theater.CurrentDate.Value.Date == currentDate)
        .Select(theater => new TheaterDetailDTO
        {
            Name = theater.Name,
            LocationId = theater.LocationId,
            Contact = theater.Contact,
            OpeningTime = theater.OpeningTime,
            ClosingTime = theater.ClosingTime,
            DoSInsideTheater = theater.DoSInsideTheater,
            DonotSInsideTheater = theater.DonotSInsideTheater,
            MovieDetailsId = theater.MovieDetailsId,
            ShowDetailsId = theater.ShowDetailsId,
            CurrentDate = theater.CurrentDate
        })
        .ToList();

    if (theatersForMovieToday.Count == 0)
    {
        
        var specificTheater = _context.TheaterDetails.FirstOrDefault(t => t.Id == 1); 
        if (specificTheater != null)
        {
            var specificTheaterDTO = new TheaterDetailDTO
            {
                Name = specificTheater.Name,
                LocationId = specificTheater.LocationId,
                Contact = specificTheater.Contact,
                OpeningTime = specificTheater.OpeningTime,
                ClosingTime = specificTheater.ClosingTime,
                DoSInsideTheater = specificTheater.DoSInsideTheater,
                DonotSInsideTheater = specificTheater.DonotSInsideTheater,
                MovieDetailsId = specificTheater.MovieDetailsId,
                ShowDetailsId = specificTheater.ShowDetailsId,
                CurrentDate = specificTheater.CurrentDate
            };

            return Ok(new
            {
                data = specificTheaterDTO,
                StatusCode = 200,
                message = "No theaters found screening '{movieName}' today. Showing details of a specific theater."
            });
        }
    }

    return Ok(new
    {
        data = theatersForMovieToday,
        StatusCode = 200,
        message ="sucesss"
    });
}

[HttpGet("highest-booking-movie/{year}")]
public IActionResult GetHighestBookingMovie(int year)
{
    var highestBookingMovie = _context.OnlineTicketBookings
        .Where(booking => booking.TicketBookingDate.HasValue && booking.TicketBookingDate.Value.Year == year)
        .GroupBy(booking => booking.MovieDetails)
        .Select(group => new
        {
            MovieName = group.Key.MovieName,
            BookingCount = group.Count()
        })
        .OrderByDescending(result => result.BookingCount)
        .FirstOrDefault();

    if (highestBookingMovie != null)
    {
        return Ok(new
        {
            MovieName = highestBookingMovie.MovieName,
            BookingCount = highestBookingMovie.BookingCount,
            StatusCode = 200,
            message = $"The movie with the highest booking count in {year} is '{highestBookingMovie.MovieName}' with {highestBookingMovie.BookingCount} bookings."
        });
    }
    else
    {
        return Ok(new
        {
            StatusCode = 200,
            message = $"No bookings found for the year {year}."
        });
    }
}
[HttpGet("MOST PREFFERED CLASS CATEGORY")]
public IActionResult GetMostPreferredClassCategory()
{
    var mostPreferredClassCategory = _context.BlocksDetails
        .GroupBy(detail => detail.ClassId)
        .Select(group => new
        {
            ClassId = group.Key,
            Count = group.Count()
        })
        .OrderByDescending(result => result.Count)
        .FirstOrDefault();

    if (mostPreferredClassCategory != null)
    {
        
        var className = _context.ClassesCategories
            .Where(c => c.Id == mostPreferredClassCategory.ClassId)
            .Select(c => c.ClassType)
            .FirstOrDefault();

        return Ok(new
        {
            MostPreferredClassCategory = className,
            Count = mostPreferredClassCategory.Count,
            StatusCode = 200,
            Message = $"The most preferred class category is '{className}' with {mostPreferredClassCategory.Count} occurrences."
        });
    }
    else
    {
        return Ok(new
        {
            StatusCode = 200,
            Message = "No data found in BlocksDetail."
        });
    }
}
[HttpGet("GetAvailableAndUnavailableSeats")]
public IActionResult GetAvailableAndUnavailableSeats()
{
    var seatCounts = _context.SeatDetails
        .GroupBy(detail => detail.IsAvailableSeats)
        .Select(group => new
        {
            IsAvailable = group.Key ?? false,
            Count = group.Count()
        })
        .ToList();

    int availableSeats = 0;
    int unavailableSeats = 0;

    foreach (var seatCount in seatCounts)
    {
        if (seatCount.IsAvailable)
        {
            availableSeats = seatCount.Count;
        }
        else
        {
            unavailableSeats = seatCount.Count;
        }
    }

    return Ok(new
    {
        AvailableSeats = availableSeats,
        UnavailableSeats = unavailableSeats,
        StatusCode = 200,
        Message = "Seat counts retrieved successfully."
    });
}
[HttpGet("positive feedback")]
public IActionResult GetPositiveAndNegativeFeedback(int thresholdRating)
{
    var feedbackCounts = _context.TheaterReviews
        .GroupBy(review => review.Ratings)
        .Select(group => new
        {
            Ratings = group.Key ?? 0,
            Count = group.Count()
        })
        .ToList();

    int positiveFeedbackCount = 0;
    int negativeFeedbackCount = 0;

    foreach (var feedbackCount in feedbackCounts)
    {
        if (feedbackCount.Ratings >= thresholdRating)
        {
            positiveFeedbackCount += feedbackCount.Count;
        }
        else
        {
            negativeFeedbackCount += feedbackCount.Count;
        }
    }

    return Ok(new
    {
        PositiveFeedbackCount = positiveFeedbackCount,
        NegativeFeedbackCount = negativeFeedbackCount,
        StatusCode = 200,
        Message = "Feedback counts retrieved successfully."
    });
}
[HttpGet("GetUserOfferStatus")]
public IActionResult GetUserOfferStatus(long userId, long offerId)
{
    var userOffer = _context.UserBuyOffers
        .FirstOrDefault(ubo => ubo.UserId == userId && ubo.OfferId == offerId);

    if (userOffer == null)
    {
        return NotFound("User offer not found.");
    }

    return Ok(new
    {
        UserId = userOffer.UserId,
        OfferId = userOffer.OfferId,
        IsGetOffer = userOffer.IsGetOffer,
        Status = userOffer.Status,
        StatusCode = 200,
        Message = "User offer status retrieved successfully."
    });
}
[HttpGet("availableTheatre")]
    public IActionResult GetAvailableTheaters()
    {
        
        DateTime currentDate = DateTime.Now;

        var availableTheaters = _context.TheaterDetails
            .Where(theater =>
                theater.OpeningTime.HasValue &&
                theater.ClosingTime.HasValue &&
                theater.OpeningTime.Value <= currentDate &&
                theater.ClosingTime.Value >= currentDate)
            .ToList();

        return Ok(new
        {
            data = availableTheaters,
            StatusCode = 200,
            message = "Available theaters retrieved successfully for the current date."
        });
    }
[HttpGet("count-parking-vehicles")]
    public IActionResult CountParkingVehicles()
    {
        try
        {
           
            int totalParkingVehicles = _context.ParkingVechicles
                .Sum(parking => parking.TotalNoVechiclesParked ?? 0);

            return Ok(new
            {
                TotalParkingVehicles = totalParkingVehicles,
                Status = 200,
                Message = "Total parking vehicles counted successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                TotalParkingVehicles = 0,
                Status = "error",
                Message = "An error occurred while counting parking vehicles."
            });
        }
    }
[HttpGet("available-parkingblocks")]
    public IActionResult GetAvailableBlocks()
    {
        try
        {
            
            var availableBlocks = _context.VechicleParkBlocks
                .Where(block => block.IsAvailable == true)
                .Select(block => new
                {
                    block.Id,
                    
                    block.BlockName,
                    block.BlockNo,
                    block.StandNo,
                    
                })
                .ToList();

            return Ok(new
            {
                AvailableBlocks = availableBlocks,
                Status = "success",
                Message = "Available blocks retrieved successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                AvailableBlocks = new List<object>(),
                Status = "error",
                Message = "An error occurred while fetching available blocks."
            });
        }
  }
[HttpGet("most-parked-vehicle-type")]
    public IActionResult GetMostParkedVehicleType()
    {
        try
        {
            
            var mostParkedVehicleType = _context.VechicleParkBlocks
                .GroupBy(block => block.VechicleTypeId)
                .Select(group => new
                {
                    VechicleTypeId = group.Key,
                    TotalParked = group.Count()
                })
                .OrderByDescending(item => item.TotalParked)
                .FirstOrDefault();

            if (mostParkedVehicleType != null)
            {
                
                var vehicleType = _context.VechicleTypes
                    .FirstOrDefault(vt => vt.Id == mostParkedVehicleType.VechicleTypeId)?.VechicleWheelType;

                return Ok(new
                {
                    VehicleType = vehicleType,
                    TotalParked = mostParkedVehicleType.TotalParked,
                    Status = "sucess",
                    Message = "Most parked vehicle type retrieved successfully."
                });
            }
            else
            {
                return Ok(new
                {
                    VehicleType = "N/A",
                    TotalParked = 0,
                    Status = "sucess",
                    Message = "No parked vehicles found."
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                VehicleType = "N/A",//placeholder no values 
                TotalParked = 0,
                Status = "error",
                Message = "An error occurred while fetching the most parked vehicle type."
            });
        }
    }

[HttpPost("helpdeskqueries")]
public IActionResult PostHelpDeskQuery([FromBody] HelpDeskQueryDTO helpDeskQueryDTO)
{
    if (helpDeskQueryDTO == null)
    {
        return BadRequest("Invalid data");
    }

   
    var helpDeskQuery = new HelpDeskQuery
    {
        UserId = helpDeskQueryDTO.UserId,
        Queries = helpDeskQueryDTO.Queries,
        
        
    };

   
    _context.HelpDeskQueries.Add(helpDeskQuery);
    _context.SaveChanges();

  
    return Ok(new
    {
        Data = helpDeskQuery,
        StatusCode = 200,
        Message = "HelpDeskQuery created successfully."
    });
}
[HttpGet("querycounts")]
public IActionResult GetHelpDeskQueries()
{
    var queries = _context.HelpDeskQueries.ToList();

    
    return Ok(new
    {
        Data = queries,queries.Count,
        StatusCode = 200,
        Message = "HelpDeskQueries retrieved successfully."
    });
}
[HttpGet("which user send more query")]
public IActionResult GetUserWithMostQueries()
{
    var userWithMostQueries = _context.HelpDeskQueries
        .GroupBy(query => query.UserId)
        .Select(group => new
        {
            UserId = group.Key,
            QueryCount = group.Count()
        })
        .OrderByDescending(user => user.QueryCount)
        .FirstOrDefault();

    if (userWithMostQueries == null)
    {
        return NotFound("No queries found.");
    }

    
    var userDetails = _context.UserRegistrations.FirstOrDefault(user => user.Id == userWithMostQueries.UserId);

    
    return Ok(new
    {
        UserId = userWithMostQueries.UserId,
        UserName = userDetails?.UserName, 
        QueryCount = userWithMostQueries.QueryCount,
        StatusCode = 200,
        Message = "User with the most queries retrieved successfully."
    });
}
[HttpPost("supportteam")]
public IActionResult PostQuerySupportTeam([FromBody] QuerySupportTeamDTO querySupportTeamDTO)
{
    if (querySupportTeamDTO == null)
    {
        return BadRequest("Invalid data");
    }

    
    var querySupportTeam = new QuerySupportTeam
    {
        TeamNo = querySupportTeamDTO.TeamNo,
        TeamName = querySupportTeamDTO.TeamName,
        PersonName = querySupportTeamDTO.PersonName,
        IsQueryResolution = querySupportTeamDTO.IsQueryResolution,
        ResponseStatus = querySupportTeamDTO.ResponseStatus
       
    };

   
    _context.QuerySupportTeams.Add(querySupportTeam);
    _context.SaveChanges();

   
    return Ok(new
    {
        Data = querySupportTeam,
        StatusCode = 200,
        Message = "QuerySupportTeam created successfully."
    });
}
[HttpGet("retrive support")]
public IActionResult GetQuerySupportTeams()
{
    
    var querySupportTeams = _context.QuerySupportTeams.ToList();

    
    return Ok(new
    {
        Data = querySupportTeams,querySupportTeams.Count,
        StatusCode = 200,
        Message = "QuerySupportTeams retrieved successfully."
    });
}
[HttpGet("query resolution and response status")]
public IActionResult GetQueryResolutionStatus()
{
    var queryResolutionStatus = _context.QuerySupportTeams
        .Select(team => new
        {
            TeamId = team.Id,
            TeamNo = team.TeamNo,
            TeamName = team.TeamName,
            PersonName = team.PersonName,
            IsQueryResolution = team.IsQueryResolution,
            ResponseStatus = team.ResponseStatus
        })
        .ToList();

    if (queryResolutionStatus == null || queryResolutionStatus.Count == 0)
    {
        return NotFound("No query resolution status details found.");
    }

    return Ok(new
    {
        data = queryResolutionStatus,
        StatusCode = 200,
        Message = "Query resolution status details retrieved successfully."
    });
}
[HttpGet("Notificationcount")]
        public IActionResult GetNotificationCountByDate([FromQuery] DateTime targetDate)
        {
            try
            {
                int notificationCount = _context.Notifications
                    .Count(notification => notification.IsCreatedAt.HasValue && notification.IsCreatedAt.Value.Date == targetDate.Date);

                return Ok(new
                {
                    Count = notificationCount,
                    Message = $"Total notifications sent on {targetDate:yyyy-MM-dd}: {notificationCount}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "An error occurred while retrieving notification count.",
                    Details = ex.Message
                });
            }
        }

[HttpGet("overall-cost")]
        public IActionResult GetOverallCostPerMonth()
        {
            try
            {
                var overallCostPerMonth = _context.TransactionDetails
                    .Where(transaction => transaction.TransactionDate.HasValue)
                    .GroupBy(transaction => new
                    {
                        Year = transaction.TransactionDate.Value.Year,
                        Month = transaction.TransactionDate.Value.Month
                    })
                    .Select(group => new
                    {
                        Year = group.Key.Year,
                        Month = group.Key.Month,
                        TotalCost = group.Sum(transaction => transaction.TotalAmountReceived ?? 0)
                    })
                    .OrderBy(result => result.Year)
                    .ThenBy(result => result.Month)
                    .ToList();

                return Ok(new
                {
                    Data = overallCostPerMonth,
                    Status = "success",
                    Message = "Overall cost per month retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "An error occurred while retrieving overall cost per month.",
                    Details = ex.Message
                });
            }
        }
    }







    
























        
        