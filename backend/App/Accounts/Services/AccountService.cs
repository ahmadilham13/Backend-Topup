using System.Text.Json;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using backend.Accounts.Interfaces.Repositories;
using backend.Accounts.Interfaces.Shared;
using backend.Accounts.Models.User;
using backend.Configs;
using backend.Entities;
using backend.Helpers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using backend.BaseModule.Interfaces.Shared;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using backend.BaseModule.Models.Media;

namespace backend.Accounts.Services;

public class AccountService : IAccountService
{
    private Account _account;
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;
    private readonly Recaptcha _recaptcha;
    private readonly IAccountRepo _accountRepo;
    private readonly ICacheService _cacheService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<AccountService> _localizer;

    public AccountService(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        IOptions<Recaptcha> recaptcha,
        IAccountRepo accountRepo,
        ICacheService cacheService,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<AccountService> localizer
    )
    {
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _recaptcha = recaptcha.Value;
        _accountRepo = accountRepo;
        _cacheService = cacheService;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
    {
        string cacheKey = model.Username + "_" + ipAddress;

        // if (!await checkCaptcha(model.Token))
        // {
        //     throw new AppException(_localizer["captcha_is_not_valid"], "captcha_is_not_valid");
        // }

        Account account = await _accountRepo.GetFullAccountByUsername(model.Username);

        if (account == null)
        {
            throw new AppException(_localizer["username_or_password_incorrect"], "username_or_password_incorrect");
        }

        if (account.LockedUntil != null && account.LockedUntil < DateTime.UtcNow)
        {
            account.LockedUntil = null;
            account.Status = AccountStatus.Active;
            await _accountRepo.UpdateAccount(account);
        }

        if (account.Status == AccountStatus.Locked)
        {
            throw new AppException(_localizer["account_locked"], "account_locked");
        }

        if (!BC.Verify(model.Password, account.PasswordHash))
        {
            throw new AppException(_localizer["username_or_password_incorrect"], "username_or_password_incorrect");
        }

        if (!account.IsVerified)
        {
            throw new AppException(_localizer["email_not_verified"], "email_not_verified");
        }

        // authentication successfull to generate jwt and refresh token
        string jwtToken = generateJwtToken(account);
        RefreshToken refreshToken = generateRefreshToken(ipAddress, account);

        await _accountRepo.CreateRefreshToken(refreshToken);

        // remove old refresh tokens from account
        removeOldRefreshTokens(account);

        // save new refresh token
        await _accountRepo.UpdateAccount(account);

        AuthenticateResponse response = _mapper.Map<AuthenticateResponse>(account);

        response.Role = account.Role.RoleName;
        response.Token = jwtToken;
        response.RefreshToken = refreshToken.Token;

        response.Avatar ??= new ImageUploadResponse()
            {
                ImageDefault = "static-assets/img/profile-placeholder.jpg",
                ImageBig = "static-assets/img/profile-placeholder-bg.jpg",
                ImageSmall = "static-assets/img/profile-placeholder-sm.jpg"
            };

        return response;
    }

    public async Task RevokeToken(string token, string ipAddress)
    {
        (RefreshToken refreshToken, Account account) = await getRefreshToken(token);

        // revoke token and save
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;

        await _accountRepo.UpdateAccount(account);
    }

    public async Task CheckEmailAvailability(CheckEmailRequest model)
    {
        if (await _accountRepo.CheckEmailExist(model.Email))
        {
            throw new AppException(_localizer["email_{0}_already_registered", model.Email].Value, "email_already_registered");
        }
    }

    // private method
    private async Task<bool> checkCaptcha(string token)
    {
        var client = new HttpClient();
        var response = await client.GetStringAsync($"{_recaptcha.APIAddress}?secret={_recaptcha.SecretKey}&response={token}");
        var tokenResponse = JsonSerializer.Deserialize<CaptchaResponse>(response);
        if (!tokenResponse.success)
        {
            return false;
        }

        return true;
    }

    private string generateJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim("id", account.Id.ToString())]),
            // Expires = DateTime.UtcNow.AddMinutes(15),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private RefreshToken generateRefreshToken(string ipAddress, Account account)
    {
        return new RefreshToken
        {
            Token = randomTokenString(),
            Account = account,
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    private void removeOldRefreshTokens(Account account)
    {
        account.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }

    private async Task<(RefreshToken, Account)> getRefreshToken(string token)
    {
        var refreshToken = await _accountRepo.GetRefreshToken(token, _account.Id) ?? throw new AppException(_localizer["invalid_token"], "invalid_token");
        if (!refreshToken.IsActive) throw new AppException(_localizer["invalid_token"], "invalid_token");
        return (refreshToken, _account);
    }

    private string randomTokenString()
    {
        var rngProvider = RandomNumberGenerator.Create();
        var randomBytes = new byte[40];
        rngProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }
}