using backend.BaseModule.Models.Base;

namespace backend.Vouchers.Models.Request;

public class VoucherFilter : BaseFilter
{
    public VoucherOrder order { get; set; }
}