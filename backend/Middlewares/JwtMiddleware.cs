using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using backend.Configs;
using backend.Entities;
using backend.Helpers;

namespace backend.Middlewares;

public class JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
{
    private readonly RequestDelegate _next = next;
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task Invoke(HttpContext context, DataContext dataContext)
    {
        string token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await attachAccountToContext(context, dataContext, token);
        }

        await _next(context);
    }

    private async Task attachAccountToContext(HttpContext context, DataContext dataContext, string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            Guid accountId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            Account account = await dataContext.Accounts.Include(x => x.RefreshTokens).Where(x => x.Id == accountId).FirstAsync();

            // attach account to context on successful jwt validation
            context.Items["Account"] = account;
        }
        catch
        {
            // throw new UnauthorizedAccessException("Unauthorized");
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
        }
    }
}