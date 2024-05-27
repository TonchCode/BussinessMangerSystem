using System.Net;
using System.Text;

namespace BMAPI.Authentication
{
    public class ApiAuth
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IConfiguration _config;

        public ApiAuth(RequestDelegate requestDelegate, IConfiguration config)
        {
            _requestDelegate = requestDelegate;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiConstants.APIHeaderKey, out var actualValue))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("\tApi Key is missing!");
                return;
            }

            if (!actualValue.Equals(_config.GetValue<string>(ApiConstants.APISecret)))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("\tApi Key is invalid!");
                return;
            }

            await _requestDelegate(context);
        }
    }
}
