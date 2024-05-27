using System.ComponentModel.DataAnnotations;

namespace backend.Accounts.Models.User;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Token { get; set; }
    public string Otp { get; set; }
}