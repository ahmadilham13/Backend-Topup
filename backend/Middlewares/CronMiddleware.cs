using Microsoft.Extensions.Options;
using backend.Configs;
using backend.Helpers;

namespace backend.Middlewares;

public class CronMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
{
    private readonly RequestDelegate _next = next;
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task Invoke(HttpContext context, DataContext dataContext)
    {
        if (context.Request.Path.StartsWithSegments("/api/cron", StringComparison.OrdinalIgnoreCase))
        {
            var token = _appSettings.InternalSecret;
            var requestToken = context.Request.Headers["x-access-token"].FirstOrDefault();

            if (token != requestToken)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");

                return;
            }
        }

        await _next(context);
    }
}