namespace TheatreBookingApp;
public interface IServices{
    public ResponseDTO GetMostOrderedFoodItemsForYesterday();
    public ResponseDTO GetHighestSellingMoviesByYear(int year);
    
}