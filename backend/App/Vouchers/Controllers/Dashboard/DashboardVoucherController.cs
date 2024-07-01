using backend.BaseModule.Controllers;
using backend.BaseModule.Models.Base;
using backend.Helpers;
using backend.Vouchers.Interfaces.Shared;
using backend.Vouchers.Models.Request;
using backend.Vouchers.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace backend.Vouchers.Controllers;

[Authorize("dashboard-voucher")]
[ApiController]
[Route("api/dashboard/voucher")]
public class DashboardVoucherController(
        IVoucherService voucherService
    ) : BaseController
{
    private readonly IVoucherService _voucherService = voucherService;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedResponse<VoucherResponse>>> GetPaginatedVoucher([FromQuery] VoucherFilter filter )
    {
        PaginatedResponse<VoucherResponse> vouchers = await _voucherService.GetPaginatedVoucher(filter);
        return Ok(vouchers);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<VoucherSingleResponse>> GetVoucherById(Guid id)
    {
        VoucherSingleResponse voucher = await _voucherService.GetVoucherById(id);
        return Ok(new { message = "success", data = voucher });
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<VoucherResponse>> CreateVoucher(CreateVoucherRequest model)
    {
        VoucherResponse voucher = await _voucherService.CreateVoucher(model, ipAddress());
        return Ok(new { message = "success", data = voucher });
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<VoucherResponse>> UpdateVoucher(Guid id, UpdateVoucherRequest model)
    {
        VoucherResponse voucher = await _voucherService.UpdateVoucher(id, model, ipAddress());
        return Ok(new { message = "success", data = voucher });
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<VoucherResponse>> DeleteVoucher(Guid id)
    {
        await _voucherService.DeleteVoucher(id, ipAddress());
        return Ok(new { message = "success" });
    }
}