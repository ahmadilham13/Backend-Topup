using backend.Accounts.Interfaces.Shared;
using backend.Accounts.Models.User;
using backend.BaseModule.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace backend.Accounts.Controllers;

[ApiController]
[Route("api")]
public class AuthController(
    IAccountService accountService,
    IStringLocalizer<AuthController> localizer
        ) : BaseController
{
    private readonly IAccountService _accountService = accountService;
    private readonly IStringLocalizer<AuthController> _localizer = localizer;

    [HttpPost("authenticate")]
    [Produces("application/json")]
    public async Task<ActionResult<AuthenticateResponse>> authenticate(AuthenticateRequest model)
    {
        var response = await _accountService.Authenticate(model, ipAddress());

        return Ok(new { message = "success", data = response });
    }

    [HttpPost("check-email")]
    [Produces("application/json")]
    public async Task<IActionResult> CheckEmail(CheckEmailRequest model)
    {
        await _accountService.CheckEmailAvailability(model);
        return Ok(new { message = "Available" });
    }
}