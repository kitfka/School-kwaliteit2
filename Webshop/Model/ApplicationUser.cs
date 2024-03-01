using System.ComponentModel.DataAnnotations;

namespace Webshop.Model;

public class ApplicationUser : BaseEntity
{
    public string Name { get; set; } = "";
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [Required]
    // source: https://regexpattern.com/email-address/
    [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = "";


    public string Password { get; set; } = "";

    public string[] Roles { get; set; } = ["Everyone"];

    public bool IsActive { get; set; } = false;
    public string Token { get; set; } = "";

}
