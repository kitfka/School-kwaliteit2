using LiteDB;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Webshop.Model;

namespace Webshop.Api;

public class AuthService : IAuthService
{
    private readonly LiteDBService _liteDBService;
    private readonly IConfiguration _configuration;

    public ILiteCollection<ApplicationUser> Users => _liteDBService.data.GetCollection<ApplicationUser>();

    public AuthService(LiteDBService liteDBService, IConfiguration configuration)
    {
        _liteDBService = liteDBService;
        _configuration = configuration;

        Users.EnsureIndex(x => x.Email, true);
    }

    /// <summary>
    /// Will try login. Only use user if the method returns true!
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="Exception">Might trigger if the JWT:SecretKey configuration value is empty.</exception>
    public bool TryLogin(string email, string password, out ApplicationUser user)
    {
        user = Users.FindOne(usr => usr.Email.ToLower() == email.ToLower());

        if (user == null) { return false; }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) { return false; }

        var tokenHandler = new JwtSecurityTokenHandler();
        string? sKey = _configuration["JWT:SecretKey"];
        if (sKey == null) 
        {
            // Should be cought by program.cs
            return false;
        } 

        var key = Encoding.ASCII.GetBytes(sKey);

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim(ClaimTypes.Role, user.Name),
        ];

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            IssuedAt = DateTime.UtcNow,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);
        user.IsActive = true;

        return true;
    }


    /// <summary>
    /// Register user and mutate the user object. Use the returntype if user id was needed after user registration
    /// </summary>
    /// <param name="user">User to register</param>
    /// <returns>User with internal ID</returns>
    public ApplicationUser Register(ApplicationUser user)
    {
        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // cool a mutation
            var userID = Users.Insert(user);

            return Users.FindById(userID);
        }
        catch (Exception)
        {
            return new ApplicationUser();
        }
    }


    public ApplicationUser GetUserFromClaims(IEnumerable<Claim> claims)
    {
        foreach (Claim claim in claims)
        {
            if (claim.Type == "nameid")
            {
                return Users.Find(x => x.Email == claim.Value).FirstOrDefault(new ApplicationUser());
            }
        }

        return new();
    }


    public ApplicationUser FindByEmail(string email)
    {
        return Users.Find(x => x.Email == email).FirstOrDefault(new ApplicationUser());
    }
}
