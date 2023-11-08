namespace TheatreBookingApp;

public class UserRegistrationDto
{
    
    public string User_Name { get; set; }
    public string Password { get; set; }
    public long Contact { get; set; }
    public string E_Mail { get; set; }
    public string Prefered_Location { get; set; }
    public string Near_by_theater_name { get; set; }
    public bool Is_Membership_card_available { get; set; }
    
    public bool Deleted_At { get; set; }
}
public class Login{
     public string E_Mail { get; set; }
     public string Password { get; set; }
}
public class MovieDetailsDto
{
    public string MovieName { get; set; }
    public string MovieDescription { get; set; }
    public string MovieReview { get; set; }
    public string MovieType { get; set; }
    public long? FdfsId { get; set; }
    public bool? FdfsIsAvailable { get; set; }
    public int? MovieRunnableDays { get; set; }
    public bool? IsAvailable { get; set; }
     
}

public partial class FirstdayFirstshowDetailDTO
{
    

    public string? MovieName { get; set; }

    public DateTime? MovieReleasingDate { get; set; }

    public DateTime? OnlineReservationOpeningTime { get; set; }

    public DateTime? OnlineResrvationClosingTime { get; set; }
}
public class ShowDetailsDTO
{
   
    public string ShowType { get; set; }
    public long ShowAvailableTypeID { get; set; }
    public long RoomID { get; set; }
}

public class ShowAvailableTypesDTO
{
    public long ID { get; set; }
    public long MovieID { get; set; }
    public string AvailableShowTimes { get; set; }
}

public class RoomDetailsDTO
{
   
    public int RoomNo { get; set; }
    public long BlockID { get; set; }
    
}

public class BlocksDetailsDTO
{
   
    public string BlockCategory { get; set; }
    public long SeatID { get; set; }
    public long ClassID { get; set; }
    public bool IsAvailable { get; set; }
}

public class SeatDetailsDTO
{
    public int SeatNO { get; set; }
    public int RowNO { get; set; }
    public long SeatCategoryID { get; set; }
    public bool IsAvailableSeats { get; set; }
    public string Status { get; set; }
}

public class ClassesCategoriesDTO
{
   
    public string ClassType { get; set; }
}

public class SeatCategoryDTO
{
 
    public string SeatType { get; set; }
}

public class FoodItemsDTO
{
   
    public string TypeFood { get; set; }
    public string ListsOfFoods { get; set; }
    public bool IsAvailable { get; set; }
    // public DateTime FoodItemsCreatedAt { get; set; }
     public long? FoodCost { get; set; }
}
public class OrderItemsDTO

{
    

    public long UserID { get; set; }
    public long FoodItemsID { get; set; }
    public long QtyItem { get; set; }
    public long TotalCost { get; set; }
    public long FoodCounterID { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }
      public DateTime? OrederDate { get; set; }
}
public class FoodCounterDTO
{    public string Name { get; set; }
    public string DirectionFromMovieRoom { get; set; }
}
public class TicketCancellationDTO
{
    
    public long TicketBookingID { get; set; }
    public DateTime DateOfCancellation { get; set; }
    public bool IsRefundable { get; set; }
}
public class TheaterReviewsDTO
{
   
    public long UserID { get; set; }
    public int Ratings { get; set; }
    public string Feedback { get; set; }
}
public class EmployeeDetailsDTO
{
    
    public string Name { get; set; }
    public string Role { get; set; }
    public long Contact { get; set; }
}
public class OfferDetailDTO
{
   
    public string OfferType { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    // public DateTime OfferFromDate { get; set; }
    // public DateTime OfferEndDate { get; set; }

  
}
public partial class UserBuyOfferDTO
{
   

    public long? UserId { get; set; }

    public long? OfferId { get; set; }

    public bool? IsGetOffer { get; set; }

    public string? Status { get; set; }

}
public class StateDetailDTO
{
   

    public string? StateName { get; set; }

    public long? CountryId { get; set; }
}
public  class CountryDetailDTO
{
  

    public string? CountryName { get; set; }}

    public partial class DistrictDetailDTO
{
  

    public string? DistrictName { get; set; }

    public long? StateId { get; set; }}

public class LocationDetailDTO
{
   

    public string? Name { get; set; }

    public long? DistrictName { get; set; }

    public long? Pincode { get; set; }}

public  class TheaterDetailDTO
{
    

    public string? Name { get; set; }

    public long? LocationId { get; set; }

    public long? Contact { get; set; }

    

    public DateTime? OpeningTime { get; set; }

    public DateTime? ClosingTime { get; set; }

    public string? DoSInsideTheater { get; set; }

    public string? DonotSInsideTheater { get; set; }

     public long? MovieDetailsId { get; set; }

    public long? ShowDetailsId { get; set; }
    public DateTime? CurrentDate { get; set; }
}

public partial class TheaterReviewDTO
{
   

    public long? UserId { get; set; }

    public int? Ratings { get; set; }

    public string? Feedback { get; set; }
     public long? TheaterId { get; set; }

    
}

public  class OnlineTicketBookingDTO
{
   

    public long? UserId { get; set; }

    public DateTime? TicketBookingDate { get; set; }
    public bool?  IsNeedCabBooking {get;set;}
    public bool?  IsNeedParkingVechicle{get;set;}
    public long ? TotalAmountCollected{get;set;}
    public long? TheaterDetails { get; set; }

    public long? OnlineTicketCounter { get; set; }

    public long? OrderItemId { get; set; }

    public long? IsParkingVechicle { get; set; }

    public long? IsCabBooking { get; set; }

  

    public string? Status { get; set; }

    public string? ScannedCopy { get; set; }
    
    public long? MovieDetailsId { get; set; }

    

}
public partial class ParkingVechicleDTO
{
 

    public long? VechicleNumber { get; set; }

    public long? BlockId { get; set; }

    public int? TotalNoVechiclesParked { get; set; }

    
}
public partial class VechicleTypeDTO
{
   

    public string? Name { get; set; }

    public string? VechicleWheelType { get; set; }

   
}
public partial class VechicleParkBlockDTO
{
    

    public long? VechicleTypeId { get; set; }

    public string? BlockName { get; set; }

    public int? BlockNo { get; set; }

    public int? StandNo { get; set; }

    public bool? IsAvailable { get; set; }

   
}
public partial class CabFacilityDTO
{
   

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? PersonCapacity { get; set; }

    public int? CostOfCab { get; set; }

    public bool? IsAvailable { get; set; }

}
public partial class CabBookingDTO
{
   

    public long? UserId { get; set; }

    public long? PickupLocation { get; set; }

    public bool? IsApplicableForUpAndDown { get; set; }

    public DateTime? PickupDateAndTime { get; set; }

    public long? EmployeeId { get; set; }

    public long? CabFacilityId { get; set; }

    public bool? IsPaidAmount { get; set; }

    
}
public  class EmployeeDetailDTO
{
   

    public string? Name { get; set; }

    public string? Role { get; set; }

    public long? Contact { get; set; }}

public  class TransactionDetailDTO
{
   

    public long? BookingId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public long? ModeOfPayment { get; set; }

    public int? TotalAmountReceived { get; set; }

    public long? TransactionId { get; set; }

}
public class ModeOfPaymentDTO
{
    
    // public long Id { get; set; }
    public string? TypeOfPaymentMode { get; set; }

    
}

public partial class PaymentDetailDTO
{
  

    public long? TransactionDetailsId { get; set; }

    public string? Status { get; set; }

    public string? InvoiceDetails { get; set; }
}
public partial class NotificationDTO
{
   
    public long? BookingId { get; set; }

    public string? NotificationType { get; set; }

    public string? Status { get; set; }

    public DateTime? IsCreatedAt { get; set; }

    
}
public partial class HelpDeskQueryDTO
{
   

    public long? UserId { get; set; }

    public string? Queries { get; set; }

    
}
public partial class QuerySupportTeamDTO
{
  

    public int? TeamNo { get; set; }

    public string? TeamName { get; set; }

    public string? PersonName { get; set; }

    public bool? IsQueryResolution { get; set; }

    public string? ResponseStatus { get; set; }
}