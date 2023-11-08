
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("Payment")]

    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }


[HttpPost("modeofpayments")]
public IActionResult CreateModeOfPayment([FromBody] ModeOfPaymentDTO modeOfPaymentDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    
    var modeOfPayment = new ModeOfPayment
    {
        TypeOfPaymentMode = modeOfPaymentDTO.TypeOfPaymentMode
       
    };

   
    _context.ModeOfPayments.Add(modeOfPayment);
    _context.SaveChanges();

    return Ok(new
    {
        data = modeOfPayment,
        StatusCode = 200,
        message = "Mode of payment created successfully."
    });
}
[HttpGet("modeofpayments")]
public IActionResult GetModeOfPayments()
{
    var modeOfPayments = _context.ModeOfPayments.ToList();

    
    var totalCount = modeOfPayments.Count;

    return Ok(new
    {
        data = modeOfPayments,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Mode of payments retrieved successfully."
    });
}
[HttpPost("transactiondetails")]
public IActionResult CreateTransactionDetail([FromBody] TransactionDetailDTO transactionDetailDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

  
    var transactionDetail = new TransactionDetail
    {
        BookingId = transactionDetailDTO.BookingId,
        TransactionDate = transactionDetailDTO.TransactionDate.HasValue ? DateTime.SpecifyKind(transactionDetailDTO.TransactionDate.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        ModeOfPayment = transactionDetailDTO.ModeOfPayment,
        TotalAmountReceived = transactionDetailDTO.TotalAmountReceived,
        TransactionId = transactionDetailDTO.TransactionId
        
    };

    _context.TransactionDetails.Add(transactionDetail);
    _context.SaveChanges();

    return Ok(new
    {
        data = transactionDetail,
        StatusCode = 200,
        message = "Transaction detail created successfully."
    });
}
[HttpGet("transactiondetails")]
public IActionResult GetTransactionDetails()
{
    var transactionDetails = _context.TransactionDetails.ToList();

  
    var totalCount = transactionDetails.Count;

    return Ok(new
    {
        data = transactionDetails,
        StatusCode = 200,
        totalCount = totalCount,
        message = "Transaction details retrieved successfully."
    });
}
[HttpPost]
        public async Task<IActionResult> CreatePaymentDetail([FromBody] PaymentDetailDTO paymentDetailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentDetail = new PaymentDetail
            {
                TransactionDetailsId = paymentDetailDTO.TransactionDetailsId,
                Status = paymentDetailDTO.Status,
                InvoiceDetails = paymentDetailDTO.InvoiceDetails
            };

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.Id }, paymentDetail);
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetailDTO>>> GetPaymentDetails()
        {
            var paymentDetails = await _context.PaymentDetails
                .Select(pd => new PaymentDetailDTO
                {
                    TransactionDetailsId = pd.TransactionDetailsId,
                    Status = pd.Status,
                    InvoiceDetails = pd.InvoiceDetails
                })
                .ToListAsync();

            return Ok(paymentDetails);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetailDTO>> GetPaymentDetail(long id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            var paymentDetailDTO = new PaymentDetailDTO
            {
                TransactionDetailsId = paymentDetail.TransactionDetailsId,
                Status = paymentDetail.Status,
                InvoiceDetails = paymentDetail.InvoiceDetails
            };

            return Ok(paymentDetailDTO);
        }
[HttpPost("Invoice")]
    public async Task<ResponseDTO> AddImage(IFormFile file, int id)
    {


        var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var filePath = "./wwwroot/Images/" + fileName;

        var sourceStream = file.OpenReadStream();
        var destinationStream = new FileStream(filePath, FileMode.Create);

        await sourceStream.CopyToAsync(destinationStream);

        var user = await _context.InvoiceDetails.AddAsync(new InvoiceDetail{
            Id = id,
            InvoiceStatus = fileName
            
        

        });

        
        await _context.SaveChangesAsync();

        return new ResponseDTO
        {
            Data = "Images/" + user,
            Message = "Your Payment copy",
            Status = TheatreBookingApp.Response.sucess

        };
        
    }

    [HttpGet("Get invoice")]
    public IActionResult GetImage(){
        var result =_context.OnlineTicketScancopies.ToList();
        return Ok (result);
    }


[HttpPost("ticketbookings")]
public IActionResult CreateOnlineTicketBooking([FromBody] OnlineTicketBookingDTO onlineTicketBookingDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // if parking and cab booking are needed
    bool isParkingNeeded = true;
    bool isCabBookingNeeded = true;


    if (isParkingNeeded && !onlineTicketBookingDTO.IsParkingVechicle.HasValue)
    {
        ModelState.AddModelError("IsParkingVechicle", "Parking details are required.");
        return BadRequest(ModelState);
    }

    if (isCabBookingNeeded && !onlineTicketBookingDTO.IsCabBooking.HasValue)
    {
        ModelState.AddModelError("IsCabBooking", "Cab booking details are required.");
        return BadRequest(ModelState);
    }

    // Calculate total amount 
    long totalAmount = CalculateTotalAmount(onlineTicketBookingDTO);

  
    var onlineTicketBooking = new OnlineTicketBooking
    {
        UserId = onlineTicketBookingDTO.UserId,
        TicketBookingDate = onlineTicketBookingDTO.TicketBookingDate,
        TheaterDetails = onlineTicketBookingDTO.TheaterDetails,
        OnlineTicketCounter = onlineTicketBookingDTO.OnlineTicketCounter,
        OrderItemId = onlineTicketBookingDTO.OrderItemId,
        IsParkingVechicle = onlineTicketBookingDTO.IsParkingVechicle,
        IsCabBooking = onlineTicketBookingDTO.IsCabBooking,
        TotalAmountCollected = totalAmount, 
        
        Status = onlineTicketBookingDTO.Status,
        ScannedCopy = onlineTicketBookingDTO.ScannedCopy,
        MovieDetailsId = onlineTicketBookingDTO.MovieDetailsId,
       
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


private long CalculateTotalAmount(OnlineTicketBookingDTO onlineTicketBookingDTO)
{
    
    long ticketPrice = 100; 

    //  if parking and cab booking are needed
    bool isParkingNeeded = true;
    bool isCabBookingNeeded = true;

    long overallcharge = 1220;
    long totalAmount = ticketPrice * overallcharge;

    //  parking price if parking is needed
    if (isParkingNeeded)
    {
        long parkingPrice = 20; 
        totalAmount += parkingPrice;
    }

    // cab booking price if cab booking is needed
    if (isCabBookingNeeded)
    {
        long cabPrice = 50; 
        totalAmount += cabPrice;
    }

    return totalAmount;
}

}



