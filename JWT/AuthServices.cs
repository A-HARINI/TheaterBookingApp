using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TheatreBookingApp.Models;

namespace TheatreBookingApp;
public class AuthServices: IAuthService {
    private readonly IConfiguration configuration;
    private readonly    AppDbContext _context;
    
    public AuthServices( IConfiguration config ,AppDbContext dbContext){
        configuration = config;
        _context = dbContext;
    }
    

     public string GetTokenGeneration(UserRegistration user){
        
        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            

            };

        var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:JwtSecretKey").Value));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var newtoken = new JwtSecurityToken(
            issuer: configuration.GetSection("Jwt:Issuer").Value,
            audience: configuration.GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(configuration.GetSection("Jwt:Time").Value)),
            signingCredentials: credential
        );

         return new JwtSecurityTokenHandler().WriteToken(newtoken) ;

    }

  

    }
 

    