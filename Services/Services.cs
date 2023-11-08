using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreBookingApp.Models;

namespace TheatreBookingApp;

public class Services : IServices
{
    private readonly AppDbContext _AppDbContext;
    public Services(AppDbContext AppDbContext)
    {
        _AppDbContext = AppDbContext;
    }
public ResponseDTO GetMostOrderedFoodItemsForYesterday()
    {
        
        DateTime yesterday = DateTime.Now.Date.AddDays(-1);

       
        var mostOrderedItems = _AppDbContext.OrderItems
            .Where(order => order.OrederDate.HasValue && order.OrederDate.Value.Date == yesterday)
            .GroupBy(order => order.FoodItemsId)
            .Select(group => new
            {
                FoodItemId = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(item => item.Count)
            .Take(5) // Get the top 5 most ordered items
            .ToList();

        // Retrieve the food item details
        var foodItemDetails = _AppDbContext.FoodItems
            .Where(foodItem => mostOrderedItems.Select(item => item.FoodItemId).Contains(foodItem.Id))
            .ToList();

        // Create the result in the specified format
        var result = mostOrderedItems.Select(item => new
        {
            FoodItem = foodItemDetails.FirstOrDefault(foodItem => foodItem.Id == item.FoodItemId)?.ListsOfFoods,
            Count = item.Count
        });

       
        var response = new ResponseDTO
        {
            Data = result,
            Status = Response.sucess,
            Message = "Most ordered food items for yesterday."
        };

        return response;
    }
public class HighestSellingMovieDetails
{
    public string MovieName { get; set; }
    public long TotalAmountCollected { get; set; }
}    
public ResponseDTO GetHighestSellingMoviesByYear(int year)
{
    try
    {
        var highestSellingMovies = _AppDbContext.OnlineTicketBookings
            .Where(booking => booking.TicketBookingDate.HasValue && booking.TicketBookingDate.Value.Year == year)
            .GroupBy(booking => booking.MovieDetailsId)
            .Select(group => new HighestSellingMovieDetails
            {
                MovieName = group.FirstOrDefault().MovieDetails.MovieName,
                TotalAmountCollected = group.Sum(booking => booking.TotalAmountCollected ?? 0)
            })
            .OrderByDescending(movie => movie.TotalAmountCollected)
            .ToList();

        return new ResponseDTO
        {
            Data = highestSellingMovies,
            Status = TheatreBookingApp.Response.sucess,
            Message = "Highest-selling movies retrieved successfully."
        };
    }
    catch (Exception ex)
    {
        // Handle any exceptions and return an error response
        return new ResponseDTO
        {
            Data = null,
            Status = TheatreBookingApp.Response.error,
            Message = "An error occurred while fetching the highest-selling movies."
        };
    }
}



}
        
        







 