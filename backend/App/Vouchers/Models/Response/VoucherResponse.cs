namespace backend.Vouchers.Models.Response;

public class VoucherResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public int Percentage { get; set; }
    public int Stock { get; set; }
}