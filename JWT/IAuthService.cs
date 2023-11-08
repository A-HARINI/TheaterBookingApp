using TheatreBookingApp.Models;

namespace TheatreBookingApp;
public interface IAuthService{
     public string GetTokenGeneration(UserRegistration user);
  
}