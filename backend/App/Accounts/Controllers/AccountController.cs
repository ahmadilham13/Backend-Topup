using backend.Accounts.Interfaces.Shared;
using backend.Accounts.Models.User;
using backend.BaseModule.Controllers;
using backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace backend.Accounts.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(
    IAccountService accountService,
    IStringLocalizer<AccountController> localizer
    ) : BaseController
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IStringLocalizer<AccountController> _localizer = localizer;

        [Authorize]
        [HttpPost("revoke-token")]
        [Produces("application/json")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            // accept token from request body
            var token = model.Token;

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = _localizer["token_not_found"] });

            // users can revoke their own tokens and admins can revoke any tokens
            if (!Account.OwnsToken(token))
                return Unauthorized(new { message = _localizer["Unauthorized"] });

            await _accountService.RevokeToken(token, ipAddress());
            return Ok(new { message = _localizer["token_revoked"] });
        }
    }