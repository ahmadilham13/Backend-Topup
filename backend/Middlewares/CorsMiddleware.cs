namespace backend.Middlewares
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _allowedOrigin;

        public CorsMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _allowedOrigin = configuration["AppSettings:MainUrl"];
        }

        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;
            var origin = context.Request.Headers["Origin"].ToString();
            // get API URL
            var currentUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
            
            // validated is _allowedOrigin is setted & origin is not empty (postman & swagger are getting null on origin headers)
            if (!string.IsNullOrEmpty(_allowedOrigin) && !string.IsNullOrEmpty(origin))
            {
                // validated is the origin are API url (because the API non Authorized getting API url on origin headers)
                if (!origin.Equals(currentUrl, StringComparison.OrdinalIgnoreCase)) 
                {
                    // validated is the origin and _allowedOrigin are same or matched
                    if (!origin.Equals(_allowedOrigin, StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("CORS policy does not allow this origin.");
                        return;
                    }
                }
                headers.Append("Access-Control-Allow-Origin", origin);
            }
            else
            {
                headers.Append("Access-Control-Allow-Origin", "*");
            }

            headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            await _next(context);
        }
    }
}