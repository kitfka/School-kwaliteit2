using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Api.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] LoginUser user)
    {
        if (String.IsNullOrEmpty(user.Email))
        {
            return BadRequest(new { message = "Email address needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password needs to entered" });
        }

        if (_authService.TryLogin(user.Email, user.Password, out ApplicationUser loggedInUser))
        {

            return Ok(new ApplicationUser() { Token = loggedInUser.Token });
        }

        return BadRequest(new { message = "User login unsuccessful" });
    }

    // POST: api/auth/register
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Register([FromBody] RegisterUser user)
    {
        if (String.IsNullOrEmpty(user.Name))
        {
            return BadRequest(new { message = "Name needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Email))
        {
            return BadRequest(new { message = "Email needs to entered" });
        }
        else if (String.IsNullOrEmpty(user.Password))
        {
            return BadRequest(new { message = "Password needs to entered" });
        }

        var roles = User.FindAll(ClaimTypes.Role);
        bool isAdmin = roles.Any(r => r.Value == "Admin");

        ApplicationUser userToRegister = new()
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password, // wut? this is not safe! it is fine, register function won't safe it!
        };

        if (isAdmin)
        {
            userToRegister.Roles = user.Role.Split(','); // fixthis
        }


        ApplicationUser registeredUser = _authService.Register(userToRegister);

        if (_authService.TryLogin(registeredUser.Email, user.Password, out ApplicationUser loggedInUser))
        {
            return Ok(new ApplicationUser() { Token = loggedInUser.Token });
        }

        return BadRequest(new { message = "User registration unsuccessful" });
    }
}
