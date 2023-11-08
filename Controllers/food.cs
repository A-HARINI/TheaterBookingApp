using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreBookingApp.Models;


namespace TheatreBookingApp;
[ApiController]
[Route("Foodorder")]

    public class FoodorderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodorderController(AppDbContext context)
        {
            _context = context;
        }



[HttpPost("fooditems/bulk")]
public IActionResult AddBulkFoodItems([FromBody] List<FoodItemsDTO> foodItems)
{
    if (foodItems == null || !foodItems.Any())
    {
        return BadRequest("Invalid food items data.");
    }

    var validationErrors = new List<string>();

    foreach (var foodItem in foodItems)
    {
        if (string.IsNullOrWhiteSpace(foodItem.TypeFood))
        {
            validationErrors.Add("TypeFood is required.");
        }

        if (string.IsNullOrWhiteSpace(foodItem.ListsOfFoods))
        {
            validationErrors.Add("ListsOfFoods is required.");
        }

        if (foodItem.FoodCost < 0)
        {
            validationErrors.Add("FoodCost cannot be negative.");
        }

        
    }

    if (validationErrors.Any())
    {
        return BadRequest(validationErrors);
    }

    var foodItemsEntities = foodItems.Select(dto => new FoodItem
    {
        TypeFood = dto.TypeFood,
        ListsOfFoods = dto.ListsOfFoods,
        IsAvailable = dto.IsAvailable,
        // FoodItemsCreatedAt = dto.FoodItemsCreatedAt,
        FoodCost = dto.FoodCost
    }).ToList();

    _context.FoodItems.AddRange(foodItemsEntities);
    _context.SaveChanges();

    return Ok(new
    {
        data = foodItemsEntities,
        StatusCode = 200,
        message = "Bulk food items created successfully."
    });
}
[HttpGet("fooditems")]
public IActionResult GetFoodItems()
{
    var foodItems = _context.FoodItems.ToList(); 

    
    var totalCount = foodItems.Count;

    return Ok(new
    {
        data = foodItems,
        totalCount = totalCount,
        StatusCode = 200,
        message = "Food items fetched successfully."
    });
}
[HttpPost("orderitems")]
public IActionResult CreateOrderItem([FromBody] OrderItemsDTO orderItemDTO)
{
    if (orderItemDTO == null)
    {
        return BadRequest("Invalid order item data.");
    }

   
    var userExists = _context.UserRegistrations.Any(u => u.Id == orderItemDTO.UserID);
    if (!userExists)
    {
        ModelState.AddModelError("UserID", "Invalid User ID.");
        return BadRequest(ModelState);
    }

    //  if the food item exists based on FoodItemsID
    var foodItemExists = _context.FoodItems.Any(f => f.Id == orderItemDTO.FoodItemsID);
    if (!foodItemExists)
    {
        ModelState.AddModelError("FoodItemsID", "Invalid Food Item ID.");
        return BadRequest(ModelState);
    }

    //  if QtyItem and TotalCost are valid)
    if (orderItemDTO.QtyItem <= 0)
    {
        ModelState.AddModelError("QtyItem", "Quantity must be greater than zero.");
        return BadRequest(ModelState);
    }

    if (orderItemDTO.TotalCost <= 0)
    {
        ModelState.AddModelError("TotalCost", "Total cost must be greater than zero.");
        return BadRequest(ModelState);
    }

    var orderItem = new OrderItem
    {
        UserId = orderItemDTO.UserID,
        FoodItemsId = orderItemDTO.FoodItemsID,
        QtyItem = orderItemDTO.QtyItem,
        TotalCost = orderItemDTO.TotalCost,
        FoodCounterId = orderItemDTO.FoodCounterID,
         OrederDate =orderItemDTO.OrederDate.HasValue ? DateTime.SpecifyKind(orderItemDTO.OrederDate.Value, DateTimeKind.Unspecified) : (DateTime?)null,
        IsPaid = orderItemDTO.IsPaid,
        IsCancelled = orderItemDTO.IsCancelled
    };

    _context.OrderItems.Add(orderItem);
    _context.SaveChanges();

    return Ok(new
    {
        data = orderItem,
        StatusCode = 200,
        message = "Order item created successfully."
    });
}
[HttpGet("orderitems/{userId}")]
public IActionResult GetOrderItemsForUser(long userId)
{
    
    var userExists = _context.UserRegistrations.Any(u => u.Id == userId);
    if (!userExists)
    {
        return NotFound("User not found.");
    }

    var orderItems = _context.OrderItems
        .Where(o => o.UserId == userId)
        .ToList();

    if (orderItems.Count == 0)
    {
        return Ok(new
        {
            data = new List<OrderItem>(),
            StatusCode = 200,
            message = "No order items found for this user.",
            TotalItems = 0,
            OverallFoodCost = 0
        });
    }

    var totalItems = orderItems.Sum(o => o.QtyItem);
    var overallFoodCost = orderItems.Sum(o => o.TotalCost);

    return Ok(new
    {
        data = orderItems,orderItems.Count,
        StatusCode = 200,
        message = "Order items retrieved successfully.",
        TotalItems = totalItems,
        OverallFoodCost = overallFoodCost
    });
}
[HttpPost("foodcounters")]
public IActionResult CreateFoodCounter([FromBody] FoodCounterDTO foodCounterDTO)
{
    if (foodCounterDTO == null)
    {
        return BadRequest("Invalid food counter data.");
    }

    var foodCounter = new FoodCounter
    {
        Name = foodCounterDTO.Name,
        DirectionFromTheMovieRoom = foodCounterDTO.DirectionFromMovieRoom
    };

    _context.FoodCounters.Add(foodCounter);
    _context.SaveChanges();

    return Ok(new
    {
        data = foodCounter,
        StatusCode = 200,
        message = "Food counter created successfully."
    });
}

[HttpGet("getfoodcounters")]
public IActionResult GetFoodCounters()
{
    var foodCounters = _context.FoodCounters.ToList();

    if (foodCounters.Count == 0)
    {
        return Ok(new
        {
            data = new List<FoodCounter>(),
            StatusCode = 200,
            message = "No food counters found."
        });
    }

    return Ok(new
    {
        data = foodCounters,
        StatusCode = 200,
        message = "Food counters retrieved successfully."
    });
}


}



