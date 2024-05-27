using System.Net;
using System.Text.Json;
using backend.Helpers;

namespace backend.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, DataContext dataContext)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            string errorCode = "";
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorCode = "not_found";
                    break;
                // case PendingApprovalException e:
                //     // data modification pending approval
                //     response.StatusCode = (int)HttpStatusCode.OK;
                //     break;
                default:
                    // unhandled error
                    // SentrySdk.CaptureException(error);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (response.StatusCode != (int)HttpStatusCode.OK)
            {
                Console.WriteLine(error?.Message);

                // await writeLog(context, dataContext, response.StatusCode, error?.Message);
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message, code = errorCode });
            await response.WriteAsync(result);
        }
    }

    // private async Task writeLog(HttpContext context, DataContext dataContext, int statusCode, string errorMessage)
    // {
    //     var requestBody = "";

    //     using (var reader = new StreamReader(context.Request.Body))
    //     {
    //         requestBody = await reader.ReadToEndAsync();
    //     }

    //     ExceptionLog exceptionLog = new ExceptionLog();

    //     exceptionLog.Account = (Account)context.Items["Account"];
    //     exceptionLog.StatusCode = statusCode;
    //     exceptionLog.RequestMethod = context.Request.Method;
    //     exceptionLog.RequestURL = context.Request.Path;
    //     exceptionLog.RequestBody = requestBody;
    //     exceptionLog.Message = errorMessage;
    //     exceptionLog.Created = DateTime.UtcNow;
    //     exceptionLog.CreatedByIp = getIPAddress(context);

    //     await dataContext.ExceptionLogs.AddAsync(exceptionLog);
    //     await dataContext.SaveChangesAsync();
    // }

    private string getIPAddress(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            return context.Request.Headers["X-Forwarded-For"];
        else
            return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}