using Microsoft.AspNetCore.Mvc;
using backend.Entities;

namespace backend.BaseModule.Controllers;

[Controller]
public abstract class BaseController : ControllerBase
{
    // returns the current authenticated account (null if not logged in)
    public Account Account => (Account)HttpContext.Items["Account"];

    protected string ipAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out Microsoft.Extensions.Primitives.StringValues value))
        {
            return value;
        }

        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
