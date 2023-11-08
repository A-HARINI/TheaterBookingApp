using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheatreBookingApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlocksDetail> BlocksDetails { get; set; }

    public virtual DbSet<CabBooking> CabBookings { get; set; }

    public virtual DbSet<CabFacility> CabFacilities { get; set; }

    public virtual DbSet<ClassesCategory> ClassesCategories { get; set; }

    public virtual DbSet<CountryDetail> CountryDetails { get; set; }

    public virtual DbSet<DistrictDetail> DistrictDetails { get; set; }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<FestivalTime> FestivalTimes { get; set; }

    public virtual DbSet<FineCollection> FineCollections { get; set; }

    public virtual DbSet<FirstdayFirstshowDetail> FirstdayFirstshowDetails { get; set; }

    public virtual DbSet<FoodCounter> FoodCounters { get; set; }

    public virtual DbSet<FoodItem> FoodItems { get; set; }

    public virtual DbSet<HelpDeskQuery> HelpDeskQueries { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<LocationDetail> LocationDetails { get; set; }

    public virtual DbSet<ModeOfPayment> ModeOfPayments { get; set; }

    public virtual DbSet<MovieDetail> MovieDetails { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OfferDetail> OfferDetails { get; set; }

    public virtual DbSet<OnlineTicketBooking> OnlineTicketBookings { get; set; }

    public virtual DbSet<OnlineTicketCounter> OnlineTicketCounters { get; set; }

    public virtual DbSet<OnlineTicketScancopy> OnlineTicketScancopies { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<ParkingVechicle> ParkingVechicles { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<QuerySupportTeam> QuerySupportTeams { get; set; }

    public virtual DbSet<RoomDetail> RoomDetails { get; set; }

    public virtual DbSet<SeatCategory> SeatCategories { get; set; }

    public virtual DbSet<SeatDetail> SeatDetails { get; set; }

    public virtual DbSet<ShowAvailableType> ShowAvailableTypes { get; set; }

    public virtual DbSet<ShowDetail> ShowDetails { get; set; }

    public virtual DbSet<StateDetail> StateDetails { get; set; }

    public virtual DbSet<TheaterDetail> TheaterDetails { get; set; }

    public virtual DbSet<TheaterReview> TheaterReviews { get; set; }

    public virtual DbSet<TicketCancellation> TicketCancellations { get; set; }

    public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }

    public virtual DbSet<UserBuyOffer> UserBuyOffers { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<VechicleParkBlock> VechicleParkBlocks { get; set; }

    public virtual DbSet<VechicleType> VechicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Theatre_Booking;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlocksDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Blocks_Details_pkey");

            entity.ToTable("Blocks_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlockCategory).HasColumnName("Block_Category");
            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.IsAvailable).HasColumnName("IS_Available");
            entity.Property(e => e.SeatId).HasColumnName("Seat_Id");
        });

        modelBuilder.Entity<CabBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cab_Booking_pkey");

            entity.ToTable("Cab_Booking");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CabFacilityId).HasColumnName("Cab_Facility_Id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.IsApplicableForUpAndDown).HasColumnName("Is_Applicable_For_Up_And_Down");
            entity.Property(e => e.IsPaidAmount).HasColumnName("Is_Paid_Amount");
            entity.Property(e => e.PickupDateAndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Pickup_Date_and_Time");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<CabFacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cab_Facilities_pkey");

            entity.ToTable("Cab_Facilities");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CostOfCab).HasColumnName("Cost_of_Cab");
            entity.Property(e => e.IsAvailable).HasColumnName("Is_Available");
            entity.Property(e => e.PersonCapacity).HasColumnName("Person_capacity");
        });

        modelBuilder.Entity<ClassesCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Classes_Categories_pkey");

            entity.ToTable("Classes_Categories");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassType).HasColumnName("Class_Type");
        });

        modelBuilder.Entity<CountryDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Country_Details_pkey");

            entity.ToTable("Country_Details");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CountryName).HasColumnName("Country_Name");
        });

        modelBuilder.Entity<DistrictDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("District_Details_pkey");

            entity.ToTable("District_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DistrictName).HasColumnName("District_Name");
            entity.Property(e => e.StateId).HasColumnName("State_ID");
        });

        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Employee_Details_pkey");

            entity.ToTable("Employee_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(220);
        });

        modelBuilder.Entity<FestivalTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Festival_Time_pkey");

            entity.ToTable("Festival_Time");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FestivalEndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Festival_End_Date");
            entity.Property(e => e.FestivalName).HasColumnName("Festival_Name");
            entity.Property(e => e.FestivalStartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Festival_Start_Date");
        });

        modelBuilder.Entity<FineCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Fine_Collection_pkey");

            entity.ToTable("Fine_Collection");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_Id");
            entity.Property(e => e.IsBookedCase).HasColumnName("Is_Booked_case");
            entity.Property(e => e.IsPaid).HasColumnName("Is_Paid");
            entity.Property(e => e.TotalCost).HasColumnName("Total_Cost");
        });

        modelBuilder.Entity<FirstdayFirstshowDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Firstday_Firstshow_Details_pkey");

            entity.ToTable("Firstday_Firstshow_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MovieName)
                .HasMaxLength(220)
                .HasColumnName("Movie_Name");
            entity.Property(e => e.MovieReleasingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Movie_Releasing_Date");
            entity.Property(e => e.OnlineReservationOpeningTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Online_Reservation_opening_time");
            entity.Property(e => e.OnlineResrvationClosingTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Online_Resrvation_Closing_time");
        });

        modelBuilder.Entity<FoodCounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Food_Counter_pkey");

            entity.ToTable("Food_Counter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DirectionFromTheMovieRoom).HasColumnName("Direction_from _the_MovieRoom");
        });

        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Food_Items_pkey");

            entity.ToTable("Food_Items");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeletdAt).HasColumnName("Deletd_At");
            entity.Property(e => e.FoodCost).HasColumnName("Food_Cost");
            entity.Property(e => e.FoodItemsCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Food_Items_Created_At");
            entity.Property(e => e.IsAvailable).HasColumnName("Is_Available");
            entity.Property(e => e.ListsOfFoods).HasColumnName("Lists_of_Foods");
            entity.Property(e => e.TypeFood).HasColumnName("Type_Food");
        });

        modelBuilder.Entity<HelpDeskQuery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Help_Desk_Queries_pkey");

            entity.ToTable("Help_Desk_Queries");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Created_At");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.ResponseBy).HasColumnName("Response_By");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Invoice_Details_pkey");

            entity.ToTable("Invoice_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InvoiceStatus).HasColumnName("Invoice_status");
        });

        modelBuilder.Entity<LocationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Location_Details_pkey");

            entity.ToTable("Location_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DistrictName).HasColumnName("District_Name");
            entity.Property(e => e.Pincode).HasColumnName("PINCode");
        });

        modelBuilder.Entity<ModeOfPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Mode_Of_Payment_pkey");

            entity.ToTable("Mode_Of_Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TypeOfPaymentMode).HasColumnName("Type_of_Payment_Mode");
        });

        modelBuilder.Entity<MovieDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Movie_Details_pkey");

            entity.ToTable("Movie_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Created_At");
            entity.Property(e => e.DeletedAt).HasColumnName("Deleted_At");
            entity.Property(e => e.FdfsId).HasColumnName("FDFS_ID");
            entity.Property(e => e.FdfsIsAvailable).HasColumnName("FDFS_is_Available");
            entity.Property(e => e.IsAvailable).HasColumnName("Is_Available");
            entity.Property(e => e.MovieDescription).HasColumnName("Movie_Description");
            entity.Property(e => e.MovieName)
                .HasMaxLength(220)
                .HasColumnName("Movie_Name");
            entity.Property(e => e.MovieReview).HasColumnName("Movie_Review");
            entity.Property(e => e.MovieRunnableDays).HasColumnName("Movie_Runnable _days");
            entity.Property(e => e.MovieType).HasColumnName("Movie_Type");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notifications_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.IsCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Is_Created_At");
            entity.Property(e => e.IsDelete).HasColumnName("Is_delete");
            entity.Property(e => e.NotificationType).HasColumnName("Notification_Type");
        });

        modelBuilder.Entity<OfferDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Offer_Details_pkey");

            entity.ToTable("Offer_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsAvailable).HasColumnName("Is_Available");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.OfferCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Offer_Created_At");
            entity.Property(e => e.OfferEndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Offer _end_date");
            entity.Property(e => e.OfferFromDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Offer_From_Date");
            entity.Property(e => e.OfferType).HasColumnName("Offer_Type");
        });

        modelBuilder.Entity<OnlineTicketBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Online_Ticket_Booking_pkey");

            entity.ToTable("Online_Ticket_Booking");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Created_At");
            entity.Property(e => e.IsCabBooking).HasColumnName("Is_Cab_booking");
            entity.Property(e => e.IsParkingVechicle).HasColumnName("Is_Parking_vechicle");
            entity.Property(e => e.MovieDetailsId).HasColumnName("Movie_Details_ID");
            entity.Property(e => e.OnlineTicketCounter).HasColumnName("Online_Ticket_Counter");
            entity.Property(e => e.OrderItemId).HasColumnName("Order_Item_Id");
            entity.Property(e => e.ScannedCopy).HasColumnName("Scanned_copy");
            entity.Property(e => e.TheaterDetails).HasColumnName("Theater_Details");
            entity.Property(e => e.TicketBookingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Ticket_Booking_Date");
            entity.Property(e => e.TotalAmountCollected).HasColumnName("Total_Amount_Collected");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.MovieDetails).WithMany(p => p.OnlineTicketBookings)
                .HasForeignKey(d => d.MovieDetailsId)
                .HasConstraintName("fk_movie_details");
        });

        modelBuilder.Entity<OnlineTicketCounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Online_Ticket_Counter_pkey");

            entity.ToTable("Online_Ticket_Counter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CounterName).HasColumnName("Counter_Name");
            entity.Property(e => e.CounterNumber).HasColumnName("Counter_Number");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");
        });

        modelBuilder.Entity<OnlineTicketScancopy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Online_Ticket_Scancopy_pkey");

            entity.ToTable("Online_Ticket_Scancopy");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ScannedCopy).HasColumnName("Scanned_Copy");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_Items_pkey");

            entity.ToTable("Order_Items");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FoodCounterId).HasColumnName("Food_counter_id");
            entity.Property(e => e.FoodItemsId).HasColumnName("Food_Items_Id");
            entity.Property(e => e.IsCancelled).HasColumnName("Is_Cancelled");
            entity.Property(e => e.IsPaid).HasColumnName("Is_Paid");
            entity.Property(e => e.OrederDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.QtyItem).HasColumnName("Qty_Item");
            entity.Property(e => e.TotalCost).HasColumnName("Total_cost");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<ParkingVechicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Parking_Vechicles_pkey");

            entity.ToTable("Parking_Vechicles");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlockId).HasColumnName("Block_ID");
            entity.Property(e => e.TotalNoVechiclesParked).HasColumnName("Total_no_vechicles_parked");
            entity.Property(e => e.VechicleNumber).HasColumnName("Vechicle_Number");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payment_Details_pkey");

            entity.ToTable("Payment_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InvoiceDetails).HasColumnName("Invoice_Details");
            entity.Property(e => e.TransactionDetailsId).HasColumnName("Transaction_Details_Id");
        });

        modelBuilder.Entity<QuerySupportTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Query_Support_Team_pkey");

            entity.ToTable("Query_Support_Team");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsQueryResolution).HasColumnName("Is_Query_Resolution");
            entity.Property(e => e.PersonName).HasColumnName("Person_name");
            entity.Property(e => e.ResponseStatus).HasColumnName("Response_Status");
            entity.Property(e => e.TeamName).HasColumnName("Team_Name");
            entity.Property(e => e.TeamNo).HasColumnName("Team_No");
        });

        modelBuilder.Entity<RoomDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Room_Details_pkey");

            entity.ToTable("Room_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlockId).HasColumnName("Block_ID");
            entity.Property(e => e.RoomNo).HasColumnName("Room_No");
        });

        modelBuilder.Entity<SeatCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Seat_Category_pkey");

            entity.ToTable("Seat_Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SeatType).HasColumnName("Seat_Type");
        });

        modelBuilder.Entity<SeatDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Seat_Details_pkey");

            entity.ToTable("Seat_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsAvailableSeats).HasColumnName("Is_Available_seats");
            entity.Property(e => e.RowNo).HasColumnName("Row_NO");
            entity.Property(e => e.SeatCategoryId).HasColumnName("Seat_Category_Id");
            entity.Property(e => e.SeatNo).HasColumnName("Seat_NO");
        });

        modelBuilder.Entity<ShowAvailableType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Show_Available_Types_pkey");

            entity.ToTable("Show_Available_Types");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AvailableShowTimes).HasColumnName("Available_Show_Times");
            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
        });

        modelBuilder.Entity<ShowDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Show_Details_pkey");

            entity.ToTable("Show_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoomId).HasColumnName("Room_Id");
            entity.Property(e => e.ShowAvailableTypeId).HasColumnName("show_available_type_id");
            entity.Property(e => e.ShowType)
                .HasMaxLength(220)
                .HasColumnName("Show_Type");
        });

        modelBuilder.Entity<StateDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("State_Details_pkey");

            entity.ToTable("State_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryId).HasColumnName("Country_ID");
            entity.Property(e => e.StateName).HasColumnName("State_Name");
        });

        modelBuilder.Entity<TheaterDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Theater_Details_pkey");

            entity.ToTable("Theater_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClosingTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Closing_Time");
            entity.Property(e => e.CurrentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Current_Date");
            entity.Property(e => e.DoSInsideTheater).HasColumnName("DO'S_Inside_Theater");
            entity.Property(e => e.DonotSInsideTheater).HasColumnName("Donot's_Inside_Theater");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.LocationId).HasColumnName("Location_Id");
            entity.Property(e => e.MovieDetailsId).HasColumnName("Movie_Details_ID");
            entity.Property(e => e.OpeningTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Opening_Time");
            entity.Property(e => e.ShowDetailsId).HasColumnName("Show_Details_ID");
            entity.Property(e => e.TheaterCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Theater_created_At");

            entity.HasOne(d => d.MovieDetails).WithMany(p => p.TheaterDetails)
                .HasForeignKey(d => d.MovieDetailsId)
                .HasConstraintName("fk_movie_details");
        });

        modelBuilder.Entity<TheaterReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Theater_Reviews_pkey");

            entity.ToTable("Theater_Reviews");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TheaterId).HasColumnName(" TheaterId");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<TicketCancellation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ticket_Cancellation_pkey");

            entity.ToTable("Ticket_Cancellation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfCancellation)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Date _of_cancellation");
            entity.Property(e => e.IsRefundable).HasColumnName("Is_Refundable");
            entity.Property(e => e.TicketBookingId).HasColumnName("Ticket_Booking_Id");
        });

        modelBuilder.Entity<TransactionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transaction_Details_pkey");

            entity.ToTable("Transaction_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_Id");
            entity.Property(e => e.ModeOfPayment).HasColumnName("Mode_of_Payment");
            entity.Property(e => e.TotalAmountReceived).HasColumnName("Total_Amount_Received");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Transaction_Date");
            entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");
        });

        modelBuilder.Entity<UserBuyOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Buy_Offer_pkey");

            entity.ToTable("User_Buy_Offer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsGetOffer).HasColumnName("Is_Get_Offer");
            entity.Property(e => e.OfferId).HasColumnName("Offer_ID");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Registration_pkey");

            entity.ToTable("User_Registration");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Account_Created_At");
            entity.Property(e => e.DeletedAt).HasColumnName("Deleted_At");
            entity.Property(e => e.EMail).HasColumnName("E-Mail");
            entity.Property(e => e.IsMembershipCardAvailable).HasColumnName("Is_Membership_card_available");
            entity.Property(e => e.NearByTheaterName)
                .HasMaxLength(220)
                .HasColumnName("Near_by_theater_name");
            entity.Property(e => e.PreferedLocation)
                .HasMaxLength(220)
                .HasColumnName("Prefered_Location");
            entity.Property(e => e.UserName)
                .HasMaxLength(220)
                .HasColumnName("User_Name");
        });

        modelBuilder.Entity<VechicleParkBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vechicle_Park_Blocks_pkey");

            entity.ToTable("Vechicle_Park_Blocks");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlockName).HasColumnName("Block_Name");
            entity.Property(e => e.BlockNo).HasColumnName("Block_No");
            entity.Property(e => e.IsAvailable).HasColumnName("Is_Available");
            entity.Property(e => e.IsCreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Is_Created_At");
            entity.Property(e => e.StandNo).HasColumnName("Stand_No");
            entity.Property(e => e.VechicleTypeId).HasColumnName("Vechicle_Type_Id");
        });

        modelBuilder.Entity<VechicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vechicle_Type_pkey");

            entity.ToTable("Vechicle_Type");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.VechicleWheelType).HasColumnName("Vechicle _Wheel_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
