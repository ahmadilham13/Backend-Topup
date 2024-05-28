using System.ComponentModel.DataAnnotations;

namespace backend.Accounts.Models.User;

public class CheckEmailRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

}