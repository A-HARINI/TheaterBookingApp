using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("Location")]

    public class LocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationController(AppDbContext context)
        {
            _context = context;
        }
[HttpPost("statedetails")]
public IActionResult CreateStateDetail([FromBody] StateDetailDTO stateDto)
{
    if (stateDto == null)
    {
        return BadRequest("Invalid state data.");
    }

   
    var stateDetail = new StateDetail
    {
        StateName = stateDto.StateName,
        CountryId = stateDto.CountryId
    };

    _context.StateDetails.Add(stateDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = stateDetail,
        StatusCode = 200,
        message = "State detail created successfully."
    });
}

[HttpGet("statedetails")]
public IActionResult GetStateDetails()
{
    
    var stateDetails = _context.StateDetails.ToList();

    return Ok(new
    {
        data = stateDetails,
        StatusCode = 200,
        message = "State details retrieved successfully."
    });
}
[HttpPost("countrydetails")]
public IActionResult CreateCountryDetail([FromBody] CountryDetailDTO countryDto)
{
    if (countryDto == null)
    {
        return BadRequest("Invalid country data.");
    }

    
    var countryDetail = new CountryDetail
    {
        CountryName = countryDto.CountryName
    };

    _context.CountryDetails.Add(countryDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = countryDetail,
        StatusCode = 200,
        message = "Country detail created successfully."
    });
}
[HttpGet("countrydetails")]
public IActionResult GetCountryDetails()
{
   
    var countryDetails = _context.CountryDetails.ToList();

    return Ok(new
    {
        data = countryDetails,
        StatusCode = 200,
        message = "Country details retrieved successfully."
    });
}
[HttpPost("districtdetails")]
public IActionResult CreateDistrictDetail([FromBody] DistrictDetailDTO districtDto)
{
    if (districtDto == null)
    {
        return BadRequest("Invalid district data.");
    }

    
    var districtDetail = new DistrictDetail
    {
        DistrictName = districtDto.DistrictName,
        StateId = districtDto.StateId
    };

    _context.DistrictDetails.Add(districtDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = districtDetail,
        StatusCode = 200,
        message = "District detail created successfully."
    });
}
[HttpGet("districtdetails")]
public IActionResult GetDistrictDetails()
{
    
    var districtDetails = _context.DistrictDetails.ToList();

    return Ok(new
    {
        data = districtDetails,
        StatusCode = 200,
        message = "District details retrieved successfully."
    });
}
[HttpPost("locationdetails")]
public IActionResult CreateLocationDetail([FromBody] LocationDetailDTO locationDto)
{
    if (locationDto == null)
    {
        return BadRequest("Invalid location data.");
    }

  
    var locationDetail = new LocationDetail
    {
        Name = locationDto.Name,
        DistrictName = locationDto.DistrictName,
        Pincode = locationDto.Pincode
    };

    _context.LocationDetails.Add(locationDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = locationDetail,
        StatusCode = 200,
        message = "Location detail created successfully."
    });
}
[HttpGet("locationdetails")]
public IActionResult GetLocationDetails()
{
    
    var locationDetails = _context.LocationDetails.ToList();

    return Ok(new
    {
        data = locationDetails,
        StatusCode = 200,
        message = "Location details retrieved successfully."
    });
}


    }



