using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email format is not valid")]
    [DefaultValue("Email")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must contain minumum 8 letter")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain one upper letter, one lower letter, one digit and one special character")]
    [DefaultValue("Password")]
    public required string Password { get; set; }
}