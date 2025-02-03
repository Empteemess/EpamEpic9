using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^[a-zA-Zა-ჰ]+$", ErrorMessage = "FirstName must contain only characters and spaces")]
    [StringLength(25, ErrorMessage = "LastName must be less than 25 characters")]
    [DefaultValue("FirstName")]

    public required string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [RegularExpression(@"^[a-zA-Zა-ჰ]+$", ErrorMessage = "LastName must contain only characters and spaces")]
    [StringLength(25, ErrorMessage = "LastName must be less than 25 characters")]
    [DefaultValue("LastName")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Wrong email format")]
    [StringLength(50, ErrorMessage = "Email must be less than 50 characters")]
    [DefaultValue("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be more than 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage =
            "The password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    [DefaultValue("Password")]
    public string Password { get; set; }

    public IEnumerable<string>? RoleNames { get; set; } = ["guest"];
}