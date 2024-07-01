using System.ComponentModel.DataAnnotations;

namespace backend.Accounts.Models.User;

public class RegisterRequest
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string WhatsappNumber { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        ErrorMessage = "Password must contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}