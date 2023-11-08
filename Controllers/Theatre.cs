using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("Theatre")]

    public class TheatreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TheatreController(AppDbContext context)
        {
            _context = context;
        }
 [HttpPost("theaterdetails")]
public IActionResult CreateTheaterDetail([FromBody] TheaterDetailDTO theaterDto)
{
    if (theaterDto == null)
    {
        return BadRequest("Invalid theater data.");
    }

    var theaterDetail = new TheaterDetail
    {
        Name = theaterDto.Name,
        LocationId = theaterDto.LocationId,
        Contact = theaterDto.Contact,
        
        
        OpeningTime = theaterDto.OpeningTime.HasValue ? DateTime.SpecifyKind(theaterDto.OpeningTime.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        ClosingTime = theaterDto.ClosingTime.HasValue ? DateTime.SpecifyKind(theaterDto.ClosingTime.Value, DateTimeKind.Unspecified) : (DateTime?)null,

        DoSInsideTheater = theaterDto.DoSInsideTheater,
        DonotSInsideTheater = theaterDto.DonotSInsideTheater,
        MovieDetailsId = theaterDto.MovieDetailsId,
        ShowDetailsId = theaterDto.ShowDetailsId,
        CurrentDate = theaterDto.CurrentDate.HasValue?DateTime.SpecifyKind(theaterDto.CurrentDate.Value, DateTimeKind.Unspecified) : (DateTime?)null,
       
    };

    _context.TheaterDetails.Add(theaterDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = theaterDetail,
        StatusCode = 200,
        message = "Theater detail created successfully."
    });
}
    
[HttpGet("theaterdetails")]
public IActionResult GetTheaterDetails()
{
    
    var theaterDetails = _context.TheaterDetails.ToList();

    return Ok(new
    {
        data = theaterDetails,
        StatusCode = 200,
        message = "Theater details retrieved successfully."
    });
}
[HttpPost("TheatreReview")]
    public IActionResult CreateTheaterReview([FromBody] TheaterReviewDTO reviewDTO)
    {
        if (reviewDTO == null)
        {
            return BadRequest("Invalid review data.");
        }

       

        var theaterReview = new TheaterReview
        {
            UserId = reviewDTO.UserId,
            Ratings = reviewDTO.Ratings,
            Feedback = reviewDTO.Feedback,
            TheaterId = reviewDTO.TheaterId

        };

        _context.TheaterReviews.Add(theaterReview);
        _context.SaveChanges();

        return Ok(new
        {
            data = theaterReview,
            StatusCode = 200,
            message = "Theater review created successfully.",
           
        });
    }

    [HttpGet("GetTheatrereview")]
    public IActionResult GetTheaterReviews()
    {
        var reviews = _context.TheaterReviews
            .Select(review => new TheaterReviewDTO
            {
                UserId = review.UserId,
                Ratings = review.Ratings,
                Feedback = review.Feedback,
                TheaterId = review.TheaterId
                
            })
            .ToList();

        return Ok(new
        {
            data = reviews,
            StatusCode = 200,
            message = "Theater reviews retrieved successfully."
        });
    }

    }

        
        