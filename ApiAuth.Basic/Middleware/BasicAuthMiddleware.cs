using ApiAuth.Basic.Services;
using System.Reflection;

namespace ApiAuth.Basic.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IBasicAuthValidation basicAuthValidation)
        {
            var userAuthKey = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(userAuthKey))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            if (!basicAuthValidation.IsValidBasicCreds(userAuthKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
