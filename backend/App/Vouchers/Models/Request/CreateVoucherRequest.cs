using System.ComponentModel.DataAnnotations;

namespace backend.Vouchers.Models.Request;

public class CreateVoucherRequest
{
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_]{5,255}$",
    ErrorMessage = "Code must contain without space")]
    public string Code { get; set; }
    [Required]
    public int Percentage { get; set; }
    [Required]
    public int Stock { get; set; }
}