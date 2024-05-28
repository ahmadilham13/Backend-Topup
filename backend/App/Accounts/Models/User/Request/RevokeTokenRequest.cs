using System.ComponentModel.DataAnnotations;

namespace backend.Accounts.Models.User;

public class RevokeTokenRequest
{
    [Required]
    public string Token { get; set; }
}