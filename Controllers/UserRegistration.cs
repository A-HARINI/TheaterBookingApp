
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
#nullable disable


[ApiController]
[Route("User Registration")]
public class RegistrationController : Controller
{
  private readonly AppDbContext _Context;

  private readonly IConfiguration configuration;
  private readonly IAuthService _authServices;

  public RegistrationController(AppDbContext Context,  IConfiguration config, IAuthService auth)
  {
    _Context = Context;
 
    configuration = config;
    _authServices = auth;
   
  }
  [HttpPost("Registration")]
  public ActionResult<UserRegistrationDto> Registration([FromBody] UserRegistrationDto user)
  {
    var result = _Context.UserRegistrations.FirstOrDefault(H => H.EMail == user.E_Mail);
    if (result != null)
    {
      return Ok("E-Mail already registered successfully");
    }
    string hash = BCrypt.Net.BCrypt.HashPassword(user.Password);
    var response = _Context.UserRegistrations.Add(new UserRegistration
    {


     
      UserName = user.User_Name,
      Contact = user.Contact,
      PreferedLocation = user.Prefered_Location,
      NearByTheaterName = user.Near_by_theater_name,
      IsMembershipCardAvailable = user.Is_Membership_card_available,
      DeletedAt = false,
      EMail = user.E_Mail,
     
      Password = hash

    });
    _Context.SaveChanges();
    return Ok(new{
        Data =response.Entity,
        StatusCode = 200,
        Message = "User details stored in database successfully"});

  }
  [HttpGet("GetRegistration details")]
  public ActionResult<UserRegistrationDto> GetRegistration()
  {
    var response = _Context.UserRegistrations.ToList();
    return Ok(new{
        Data =response,response.Count,
        StatusCode = 200,
        Message = "User details retrived in database successfully"});

  }
  [HttpPost("Login")]
public ActionResult<UserRegistrationDto> Login([FromBody] Login userlogin)
{
    var result = _Context.UserRegistrations.FirstOrDefault(n => n.EMail == userlogin.E_Mail);
    if (result == null)
    {
        return BadRequest("Email mismatch");
    }
    var hash = BCrypt.Net.BCrypt.Verify(userlogin.Password, result.Password);

    if (hash)
    {
        var token = _authServices.GetTokenGeneration(result);
        return Ok(new
        {
            token,
        });
    }

   
    return BadRequest("Invalid password");}

[HttpPost("offerdetails")]
public IActionResult CreateOfferDetail([FromBody] OfferDetailDTO offerDetailDTO)
{
    if (offerDetailDTO == null)
    {
        return BadRequest("Invalid offer detail data.");
    }

     var offerDetail = new OfferDetail
    {
        OfferType = offerDetailDTO.OfferType,
        Description = offerDetailDTO.Description,
        IsAvailable = offerDetailDTO.IsAvailable,
        // OfferFromDate = DateTime.SpecifyKind(offerDetailDTO.OfferFromDate, DateTimeKind.Utc), // Specify the DateTimeKind.Utc
        // OfferEndDate = DateTime.SpecifyKind(offerDetailDTO.OfferEndDate, DateTimeKind.Utc), // Specify the DateTimeKind.Utc
        // OfferCreatedAt = DateTime.UtcNow, // Use UtcNow to ensure UTC time
        // IsDelete = false // Assuming it's not deleted initially
    };

    _Context.OfferDetails.Add(offerDetail);
    _Context.SaveChanges();

    return Ok(new
    {
        data = offerDetail,
        StatusCode = 200,
        message = "Offer detail created successfully."
    });
}

[HttpGet("offerdetails")]
public IActionResult GetOfferDetails()
{
    var offerDetails = _Context.OfferDetails.ToList();

    if (offerDetails.Count == 0)
    {
        return Ok(new
        {
            data = new List<OfferDetail>(),
            StatusCode = 200,
            message = "No offer details found."
        });
    }

    return Ok(new
    {
        data = offerDetails,offerDetails.Count,
        StatusCode = 200,
        message = "Offer details retrieved successfully."
    });
}
[HttpPost("userbuyoffer")]
public IActionResult CreateUserBuyOffer([FromBody] UserBuyOfferDTO userBuyOfferDTO)
{
    if (userBuyOfferDTO == null)
    {
        return BadRequest("Invalid user buy offer data.");
    }

   
    var user = _Context.UserRegistrations.FirstOrDefault(u => u.Id == userBuyOfferDTO.UserId);
    if (user == null)
    {
        ModelState.AddModelError("UserId", "Invalid User ID.");
        return BadRequest(ModelState);
    }

    // Check  offer exists
    var offer = _Context.OfferDetails.FirstOrDefault(o => o.Id == userBuyOfferDTO.OfferId);
    if (offer == null)
    {
        ModelState.AddModelError("OfferId", "Invalid Offer ID.");
        return BadRequest(ModelState);
    }

    // Check if the user has booked at least 3 times
    int minBookingCountRequired = 3; 
    int userBookingCount = _Context.OrderItems.Count(oi => oi.UserId == userBuyOfferDTO.UserId);

    if (userBookingCount < minBookingCountRequired)
    {
        ModelState.AddModelError("UserId", "User must have at least 3 bookings to purchase this offer.");
        return BadRequest(ModelState);
    }

  
    int maxAllowedPurchases = 5; 
    int userPurchaseCount = _Context.UserBuyOffers.Count(ubo => ubo.UserId == userBuyOfferDTO.UserId && ubo.OfferId == userBuyOfferDTO.OfferId);
    if (userPurchaseCount >= maxAllowedPurchases)
    {
        ModelState.AddModelError("UserId", "User has reached the maximum allowed purchases for this offer.");
        return BadRequest(ModelState);
    }

    var userBuyOffer = new UserBuyOffer
    {
        UserId = userBuyOfferDTO.UserId,
        OfferId = userBuyOfferDTO.OfferId,
        IsGetOffer = userBuyOfferDTO.IsGetOffer,
        Status = userBuyOfferDTO.Status
    };

    _Context.UserBuyOffers.Add(userBuyOffer);
    _Context.SaveChanges();

    return Ok(new
    {
        data = userBuyOffer,
        StatusCode = 200,
        message = "User buy offer created successfully."
    });
}
[HttpGet("userbuyoffers")]
public IActionResult GetUserBuyOffers()
{
    
    var userBuyOffers = _Context.UserBuyOffers
        .GroupBy(ubo => ubo.UserId)
        .Select(g => new
        {
            UserId = g.Key,
            TotalCount = g.Count()
        })
        .ToList();

    return Ok(new
    {
        data = userBuyOffers,
        StatusCode = 200,
        message = "User buy offers retrieved successfully."
    });
}


}