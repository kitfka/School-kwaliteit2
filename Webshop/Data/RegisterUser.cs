using System.ComponentModel.DataAnnotations;

namespace Webshop.Data;

public class RegisterUser
{
    public string Name { get; set; } = "";

    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [Required]
    // source: https://regexpattern.com/email-address/
    [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";
    public string Role { get; set; } = "Everyone";
}